# This problem was asked by Google.
#
# You are given a series of arithmetic equations as a string, such as:
#
# y = x + 1
# 5 = x + 3
# 10 = z + y + 2
#
# The equations use addition only and are separated by newlines. Return a mapping
# of all variables to their values. If it's not possible, then return null. In
# this example, you should return:
#
# {
#   x: 2,
#   y: 3,
#   z: 5
# }

from typing import Tuple, List, Dict, Optional, Iterable
import copy
from fractions import Fraction


# a1 * var1 + a2 * var2 + ... + ak * vark + c = 0
class Polynomial:
    def __init__(self, variables: List[Tuple[Fraction, str]], c: Fraction):
        # list of tuples (coefficient, var_name)
        self.variables = [(Fraction(factor), v) for factor, v in variables]
        self.c = c

    def has_var(self, var):
        return self.get_var(var) is not None

    def get_var(self, var, pop=False):
        for idx in range(len(self.variables)):
            tuple = self.variables[idx]
            if tuple[1] == var:
                if pop:
                    del self.variables[idx]
                return tuple
        return None

    def get_var_names(self):
        return set([t[1] for t in self.variables])

    def get_vars(self):
        return self.variables

    def clone(self):
        return copy.deepcopy(self)

    # replaces the var with a known value
    def replace_var(self, var, value):
        factor, _ = self.get_var(var, pop=True)
        self.c += factor * value

    # returns new polynomial created by expressing "var" via
    # the specified polynomial and substituting it in the current one
    def substitute(self, source, replaced_var: str):
        if not self.has_var(replaced_var) or not source.has_var(replaced_var):
            raise Exception('both Polynomials should have specified variable')

        result = self.clone()

        replacement_source_factor = source.get_var(replaced_var)[0]
        replacement_target_factor = result.get_var(replaced_var, pop=True)[0]
        alpha = replacement_target_factor / replacement_source_factor
        result.c -= alpha * source.c

        # update or add vars
        for source_factor, var in source.get_vars():
            if var == replaced_var:
                continue  # this variable is being eliminated from the result!

            result_var = result.get_var(var)
            factor_diff = -alpha * source_factor

            if result_var is not None:
                new_factor = result.get_var(var, pop=True)[0] + factor_diff
                if new_factor != 0:
                    result.variables.append((new_factor, var))
            else:
                if factor_diff != 0:
                    result.variables.append((factor_diff, var))

        result.normalize()

        return result

    def normalize(self):
        # remove variables with close with 0 factor
        for idx in range(len(self.variables) - 1, -1, -1):
            factor, var = self.variables[idx]
            if factor == 0:
                print(f'\texcluding {var} from {self}')
                del self.variables[idx]

        if self.variables:
            # also, sort variables lexicographically and make sure the factor of
            # the first variable is one to avoid x = 1, 2x = 2, 3x = 3 type of duplicates
            self.variables = sorted(self.variables, key=lambda t: t[1])
            norm, _ = self.variables[0]
            self.variables = [(f/norm, v) for (f, v) in self.variables]
            self.c = self.c / norm

    def __print_var(self, factor, var):
        return self.__ensure_sign(f'{factor}*{var}')

    def __ensure_sign(self, s):
        s = str(s)
        if s[0] != '-':
            s = '+' + s
        return s

    def __repr__(self):
        s = (f' '.join([self.__print_var(*v) for v in self.variables])
             + f'{self.__ensure_sign(self.c)}=0')
        return s

    def __hash__(self):
        vars = sorted(self.get_var_names())
        return hash('_'.join(vars))

    def __eq__(self, other):
        return set(self.get_vars() + [self.c]) == set(other.get_vars() + [other.c])


# not the best parser in the world, and it will produce some crazy things
# for invalid strings, but that's not the point
def parse(s: str) -> Polynomial:
    s = s.replace(' ', '')
    c = 0
    vars = []
    idx = 0
    token = None

    # tries to match sequence of chars matching predicate
    # if successful moves the idx pointer and puts the result into the token
    # closure variable, returns True, otherwise does not move the pointer
    def accept(predicate):
        nonlocal token
        nonlocal idx

        if not predicate(s[idx]):
            return False

        token = ''

        while idx < len(s) and predicate(s[idx]):
            token += s[idx]
            idx += 1

        return True

    before_eq = True
    while idx < len(s):
        alpha = 1 if before_eq else -1
        if accept(str.isalpha):
            vars.append((alpha, token))
        elif accept(str.isdigit):
            c_temp = alpha * int(token)
            if idx < len(s) and accept(lambda ch: ch == '*'):
                if not accept(str.isalpha):
                    raise Exception('variable expected')
                vars.append((alpha * c_temp, token))
            else:
                c += c_temp
        elif accept(lambda ch: ch == '+'):
            pass  # noop
        elif accept(lambda ch: ch == '='):
            if not before_eq:
                raise Exception(f'only one eq sign is allowed')
            before_eq = False
        else:
            raise Exception(f'unexpected "{s[idx]}"')

    if len(set([var for _, var in vars])) != len(vars):
        raise Exception('every variable can only be referenced once')

    return Polynomial(vars, c)


def parse_lines(s: str) -> List[Polynomial]:
    result = []
    for line in s.splitlines(keepends=False):
        result.append(parse(line))
    return result


def solver(polynomials: Iterable[Polynomial]) -> Optional[Dict[str, float]]:
    # iteratively go via all the polynomials and produce derived ones until we figure
    # out all the values
    # polynomials with 1 variable are evaluated into the results -- that's the
    # end step
    polynomials = list(polynomials)
    values = {}  # var -> number
    unknowns = set.union(*[p.get_var_names() for p in polynomials])
    round = 0
    counter = 0

    while True:
        progress = False
        round += 1
        new_polynomials = set()

        # print(f'** ROUND #{round} (polynoms: {len(polynomials)})...')

        # check if all is solved
        if len(values) == len(unknowns):
            break

        def preprocess():
            nonlocal polynomials
            # normalize everything
            # remove solved polynomials
            for p in polynomials:
                p.normalize()

            for p in list(polynomials):
                if len(p.variables) == 0:
                    polynomials.remove(p)

        def add_polynomial(p):
            if p not in polynomials:
                # print(f'\tadding {new_polynomial}')
                polynomials.append(p)

        def remove_polynomial(p):
            if p not in polynomials:
                # print(f'\tremoving {new_polynomial}')
                polynomials.remove(p)

        preprocess()

        for p in list(polynomials):
            counter += 1
            # print(f'[{counter}] handling {p}...')

            # 1. see if we have some solved polynomials
            if len(p.variables) == 1:
                progress = True
                factor, var = p.variables[0]
                c = p.c
                value = -c / factor
                if var not in values:
                    values[var] = value
                    # print(f'\t!! {var} = {value}')
                elif values[var] != value:
                    # print(f'INCOMPATIBLE! {values[var]} != {value}')
                    return None  # inconsistent!
                remove_polynomial(p)  # remove such polynomial, as it's no longer useful
                continue  # go to the next one!

            # 2. check if we can replace the calculated variable with value
            for known_var in values:
                if p.has_var(known_var):
                    progress = True
                    p.replace_var(known_var, values[known_var])

            # 3. infer other polynomials by trying to express on variable via other
            #    take one polynomial and for each unknown variable, try to combine
            #    with other polynomials expressing the variable from the given
            #    polynomial and replacing it in others > producing more derived ones
            if len(p.variables) >= 2:
                # take the first variable
                factor, var = p.get_vars()[0]

                # express var with other unknowns and stick into other polynomials
                # that have it, producing a new ones
                if factor == 0:
                    p.get_var(var, pop=True)
                    continue

                for p2 in list(polynomials):
                    if p is p2 or len(p2.variables) <= 1 or not p2.has_var(var):
                        continue

                    # try to stick var expressed via "p" to p2
                    new_polynomial = p2.substitute(p, var)
                    if len(new_polynomial.variables) > 0:
                        add_polynomial(new_polynomial)
                        progress = True

        # add new polynomials to the working set
        polynomials.extend(new_polynomials)

        if not progress:
            break

    return values


def solve(s: str):
    polynomials = parse_lines(s)
    print(polynomials)
    return solver(polynomials)


# I think i went crazy on this one, but... i just forgot how to solve system of linear
# equations and had to improvise
if __name__ == '__main__':
    def psolve(*args):
        result = solve(*args)
        if result is None:
            print('\tUNSOLVABLE')
        else:
            dic = {k: float(result[k]) for k in result}
            print(f'\tSOLVED: {dic}')

    psolve("""y = x + 1
        5 = x + 3
        10 = z + y + 2""")

    # not what problem statement asks to support
    psolve("""1*x + 2*y + 3*z = 14
        x + y + z = 6
        2*z = y + x + 3""")

    psolve("""2*x + 4*y + 3*z = 9
        5*x + 9*y + z = 15
        2*z = y + x""")

    psolve("""1*x = 2
        2*x = 3""")
