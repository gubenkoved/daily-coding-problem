# This problem was asked by Quantcast.

# You are presented with an array representing a Boolean expression.
# The elements are of two kinds:

# T and F, representing the values True and False.
# &, |, and ^, representing the bitwise operators for AND, OR, and XOR.
# Determine the number of ways to group the array elements using
# parentheses so that the entire expression evaluates to True.

# For example, suppose the input is ['F', '|', 'T', '&', 'T'].
# In this case, there are two acceptable groupings: (F | T) & T and F | (T & T).


# so... the first thing we need to do is an ability to evaluate these
# expressions; secondly we need to understand how to add paranthesis so that
# expression remains valid; then we can simply generate all the variants and 
# check if they evaluate to true

def generate(expression: list):
    generated = set()
    n = len(list(filter(lambda x: x == 'T' or x == 'F', expression)))

    generate_inner(expression, n - 1, 0, len(expression) - 1, generated)

    # remove itself
    # root = f"({''.join(expression)})"
    # generated.remove(root)

    #print(f'... {len(generated)} items generated ...')

    return generated

def generate_inner(expression: list, size: int, left: int, right: int, generated: set):

    #print(f'gen {"".join(expression)} s={size}, l={left}, right={right}')

    # add the current one to the results as a possiblity
    exp_string = ''.join(expression)
    
    # if size >= 2:
    #     exp_string = exp_string[:left] + '(' + exp_string[left:right+1] + ')' + exp_string[right+1:]

    if size == 1:
        generated.add(exp_string)

    if size == 1:
        return

    def isTerm(x):
        return x == 'T' or x == 'F'

    def nextTermIdx(expression, startAt, skip):
        idx = startAt
        skipped = 0
        while True:
            if isTerm(expression[idx]):
                if skipped == skip:
                    break
                skipped += 1
            idx += 1
        return idx

    # total terms count
    k = len(list(filter(isTerm, expression[left:right+1])))

    # start pointers

    steps = 0
    i = left
    j = nextTermIdx(expression, i, size - 1)

    # pass w/o groupping on this size
    #generate_inner(expression, size - 1, i, j, generated)

    # passes with groupping for this size
    while True:
        steps += 1

        # add grouping at i and j, go next
        expression_copy = expression.copy()
        expression_copy.insert(i, '(')
        expression_copy.insert(j + 2, ')') # +2 as we already added 1 more char

        generate_inner(expression_copy, size - 1, i + 1, j + 1, generated)

        if steps > k - size:
            break

        i = nextTermIdx(expression, i, 1)
        j = nextTermIdx(expression, j, 1)

    pass

# grammar:
# EXPRESSION -> TERM | EXPRESSION '|' TERM
# TERM -> FACTOR | TERM '&' FACTOR
# FACTOR -> 'T' | 'F' | '(' EXPRESSION ')'

# recoursive decsent parser
def evaluate(s: str) -> bool:
    idx = 0

    def peek(n=1) -> str:
        if len(s) - idx < n:
            return None

        return s[idx:idx+n]

    def accept(token: str) -> bool:
        nonlocal idx

        token_len = len(token)

        if peek(token_len) == token:
            idx += token_len
            return True
        
        return False

    def parse_expression() -> bool:
        result = parse_term()

        while True:
            if accept("|"):
                or_term = parse_term()
                result = result or or_term
            break

        return result

    def parse_term() -> bool:
        result = parse_factor()

        while True:
            if accept("&"):
                and_factor = parse_factor()
                result = result and and_factor
            break

        return result

    def parse_factor() -> bool:
        if accept('T'):
            return True
        elif accept('F'):
            return False
        elif accept('('):
            result = parse_expression()

            if not accept(')'):
                raise Exception('expected to have closing braket')

            return result

        raise Exception('unable to parse')

    result = parse_expression()

    if idx < len(s):
        raise Exception('unable to parse fully -- leftover detected')

    # print(f'evaluated {s} into {result}')

    return result

def solution(s: str):
    generated = generate(list(s))
    results = list(filter(lambda x: evaluate(x), generated))
    return (s, results)

def pretty_print(expressions_list: list):
    idx = 0
    for expression in expressions_list:
        idx += 1
        print(f'{idx:3}: {"".join(expression)}')

    print('')
    pass

assert(evaluate("T") == True)
assert(evaluate("(((F)))") == False)
assert(evaluate("F&F|T") == True)
assert(evaluate("F&(F|T)") == False)

# pretty_print(generate(list("F|T&T")))
# pretty_print(generate(list("F&F|T")))
# pretty_print(generate(list("F&F|T&F")))
# pretty_print(generate(list("F|T|F|T|F")))
# pretty_print(generate(list("F|T|F|T|F|T")))
# pretty_print(generate(list("F|T|F|T|F|T|F|F|F|T|F|T")))

print(solution('F|T&T'))
print(solution('F&F|T'))
print(solution('F&F|T|F&T|F'))
print(solution('T|F|T|F'))
