# This problem was asked by Squarespace.

# Write a function, add_subtract, which alternately adds and subtracts curried
# arguments. Here are some sample operations:

# add_subtract(7) -> 7
# add_subtract(1)(2)(3) -> 1 + 2 - 3 -> 0
# add_subtract(-5)(10)(3)(9) -> -5 + 10 - 3 + 9 -> 11

def f(x=None, add=True):
    def inner(y=None):
        if y is None:
            return x
        return f(x + y if add else x - y, add=not add)
    return inner

# look at the MAGIC :)
# import inspect
# g = f(1)
# print(inspect.getclosurevars(g))
# g = g(2)
# print(inspect.getclosurevars(g))
# g = g(3)
# print(inspect.getclosurevars(g))

# how to make it work w/o some special termination case?

print(f(1)(2)())
print(f(1)(2)(3)())
print(f(-5)(10)(3)(9)())
