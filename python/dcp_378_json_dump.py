# This problem was asked by Coinbase.
#
# Write a function that takes in a number, string, list, or dictionary and returns
# its JSON encoding. It should also handle nulls.
#
# For example, given the following input:
#
# [None, 123, ["a", "b"], {"c":"d"}]
# You should return the following, as a string:
#
# '[null, 123, ["a", "b"], {"c": "d"}]'


def json_dump(o):
    if o is None:
        return 'null'
    elif isinstance(o, int):
        return str(o)
    elif isinstance(o, str):
        return '"' + o + '"'
    elif isinstance(o, list):
        return '[' + ', '.join([json_dump(x) for x in o]) + ']'
    elif isinstance(o, dict):
        return '{' + ', '.join([json_dump(k) + ': ' + json_dump(o[k]) for k in o]) + '}'
    else:
        raise Exception(f'not supported type: {type(o)}')


assert json_dump('test') == '"test"'
assert json_dump([1, 2]) == '[1, 2]'
assert json_dump(['test', 1]) == '["test", 1]'
assert json_dump({1: 'one', "2": 'two'}) == '{1: "one", "2": "two"}'
assert json_dump([None, 123, ['a', 'b'], {'c': 'd'}]) == '[null, 123, ["a", "b"], {"c": "d"}]'
