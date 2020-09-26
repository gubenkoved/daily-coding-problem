# This problem was asked by Quantcast.
#
# You are presented with an array representing a Boolean expression. The elements are
# of two kinds:
#
# T and F, representing the values True and False.
# &, |, and ^, representing the bitwise operators for AND, OR, and XOR.
# Determine the number of ways to group the array elements using parentheses so that
# the entire expression evaluates to True.
#
# For example, suppose the input is ['F', '|', 'T', '&', 'T']. In this case, there
# are two acceptable groupings: (F | T) & T and F | (T & T).


# recursive descent parser
# precedence: & > ^ > |
# not sure if this grammar is the most efficient way of putting the order
#   of operator together...
# EXPRESSION := TERM_OR | TERM_OR '|' TERM_OR
# TERM_OR := TERM_XOR | TERM_XOR '^' TERM_XOR
# TERM_XOR := TERM_AND | TERM_AND '&' TERM_AND
# TERM_AND := FACTOR | '(' EXPRESSION ')
# FACTOR := 'T' | 'F'
def eval_expr(tokens):
    cursor = 0

    def expect(token):
        if not accept(token):
            raise Exception(f'expected {token}')

    def accept(token):
        nonlocal cursor
        if cursor >= len(tokens):
            return False
        if tokens[cursor] == token:
            cursor += 1
            return True
        return False

    def parse_expression():
        result = parse_term_or()
        while accept('|'):
            result |= parse_term_or()
        return result

    def parse_term_or():
        result = parse_term_xor()
        while accept('^'):
            term_p1 = parse_term_xor()
            result ^= term_p1
        return result

    def parse_term_xor():
        result = parse_term_and()
        while accept('&'):
            result &= parse_term_and()
        return result

    def parse_term_and():
        if accept('('):
            result = parse_expression()
            expect(')')
            return result
        result = parse_factor()
        return result

    def parse_factor():
        if accept('T'):
            return True
        if accept('F'):
            return False
        raise Exception('factor expected')

    return parse_expression()


def count_factors(expression):
    return sum(1 for token in expression if token in ['T', 'F'])


def generate(expression):
    # generates the expressions that are possible to get from the original ones
    n = count_factors(expression)

    # print(f'GEN {expression}')

    if n <= 2:
        yield expression
        return

    # there are total of n factors in the expression
    # try to compose 2 groups of len n-1 and recursively dive!
    for nested in generate(expression[:-2]):
        yield ['('] + nested + [')'] + expression[-2:]

    for nested in generate(expression[2:]):
        yield expression[:2] + ['('] + nested + [')']


def count_number_of_ways_to_evaluate_to_true(expression):
    count = 0
    for gen_expr in generate(expression):
        if eval_expr(gen_expr):
            print(' '.join(gen_expr))
            count += 1
    return count


# tests parser itself
assert eval_expr(['T']) is True
assert eval_expr(['F']) is False
assert eval_expr(['F', '|', 'T']) is True
assert eval_expr(['F', '|', 'T', '&', 'T']) is True
assert eval_expr(['F', '|', 'T', '&', 'T']) is True
assert eval_expr(['(', 'F', '|', 'T', ')', '&', 'T']) is True
assert eval_expr(['T', '^', 'T']) is False
assert eval_expr(['T', '^', 'T', '&', 'F']) is True
assert eval_expr(['(', 'T', '^', 'T', ')', '&', 'F']) is False

# for exp in generate(['F', '|', 'T', '&', 'T']): print(exp)
# for exp in generate(['F', '|', 'T', '&', 'T', '^', 'F']): print(exp)

assert count_number_of_ways_to_evaluate_to_true(['F', '|', 'T', '&', 'T']) == 2
assert count_number_of_ways_to_evaluate_to_true(['F', '|', 'T', '&', 'T', '^', 'F']) == 4
