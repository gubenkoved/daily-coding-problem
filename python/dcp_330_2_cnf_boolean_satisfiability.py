# This problem was asked by Dropbox.

# A Boolean formula can be said to be satisfiable if there is a way to assign truth values to each
# variable such that the entire formula evaluates to true.

# For example, suppose we have the following formula, where the symbol ¬ is used to denote negation:

# (¬c OR b) AND (b OR c) AND (¬b OR c) AND (¬c OR ¬a)

# One way to satisfy this formula would be to let a = False, b = True, and c = True.

# This type of formula, with AND statements joining tuples containing exactly one OR, is known as 2-CNF.

# Given a 2-CNF formula, find a way to assign truth values to satisfy it, or return False if this is
# impossible.

from typing import Dict, Optional


def evaluate(formula: str, values: Dict[str, bool]) -> bool:
    terms = [t.strip().replace('(', '').replace(')', '') for t in formula.split('AND')]

    for term in terms:
        terms_vars = [v.strip() for v in term.split('OR')]
        a = terms_vars[0]
        b = terms_vars[1]
        a_val = values[a[-1]] if not a.startswith('-') else not values[a[-1]]
        b_val = values[b[-1]] if not b.startswith('-') else not values[b[-1]]

        if not(a_val or b_val):
            return False

    return True

# O(2^n) where n is amount of variables
def solve_brute_force(formula: str) -> Optional[Dict[str, bool]]:
    formula = formula.upper()
    terms = [t.strip() for t in formula.split('AND')]
    variables = []

    for term in terms:
        cur_variables = [v.replace('-', '').replace('(', '').replace(')', '').strip() for v in term.split('OR')]
        variables.extend(cur_variables)

    # remove duplicates
    variables = list(set(variables))
    values = {}
    found = False

    def search(idx: int) -> bool:
        nonlocal found

        if idx == len(variables):
            if evaluate(formula, values):
                found = True
                return True
            return False

        for val in (True, False):
            values[variables[idx]] = val

            if search(idx + 1):
                return True

    search(0)

    return values if found else None


formula = '(-c OR b) AND (b OR c) AND (-b OR c) AND (-c OR -a)'

# evaluate(formula, {'a': False, 'b': True, 'c': True})

print(solve_brute_force(formula))

print(solve_brute_force('(a or b) and (c or d) and (e or -f) and (-f or -a)'))
