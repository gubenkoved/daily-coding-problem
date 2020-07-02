# This problem was asked by Dropbox.

# Create an algorithm to efficiently compute the approximate median
# of a list of numbers.

# More precisely, given an unordered list of N numbers, find an element
# whose rank is between N / 4 and 3 * N / 4, with a high level of certainty,
# in less than O(N) time.


from typing import List
import random
import datetime


def time_it(func):
    def wrapper(*args, **kwargs):
        start = datetime.datetime.now()
        result = func(*args, **kwargs)
        elapsed = (datetime.datetime.now() - start)
        print(f'elapsed: {elapsed} for {func.__name__}')
        return result
    return wrapper

# effectively O(1), precision is unknown though :)
@time_it
def fast_median(a: List[int], sample_size: int = 1001):
    # we can simply peek predefined amount of elements at random and
    # compute a median there
    if len(a) <= sample_size:
        sample = list(a)
    else:
        sample = random.choices(a, k=sample_size)

    sample.sort()

    return sample[sample_size // 2]

@time_it
def generate_array(n: int = 1000000):
    # return list([random.randint(0, 1000) for _ in range(0, 1000000)])
    r = list([int(random.random() * 1000) for _ in range(n)])
    print(f'generated an array of size {len(r)}')
    return r


r = generate_array(n=1000 * 1000 * 10)
print(fast_median(r))
