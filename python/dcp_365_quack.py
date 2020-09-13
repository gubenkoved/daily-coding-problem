# This problem was asked by Google.
#
# A quack is a data structure combining properties of both stacks and queues. It can
# be viewed as a list of elements written left to right such that three operations
# are possible:
#
# push(x): add a new item x to the left end of the list
# pop(): remove and return the item on the left end of the list
# pull(): remove the item on the right end of the list.
#
# Implement a quack using three stacks and O(1) additional memory, so that the
# amortized time for any push, pop, or pull operation is O(1).

from collections import deque

class Quack():
    # stack 1 -- input stack
    # stack 2 -- stack to serve queue operations on request
    # stack 3 -- additional stack to serve stack operations after we moved data to stack 2
    # we also need a counter for items usable on stack 2 & 3 (which are always equal)
    # because the physical count on these stacks is not going to be representative
    # as we will have to somehow remember that we virtually excluded some amount of items
    # from the non-accessible end

    def __init__(self):
        self._count = 0
        self.input = deque()
        self.aux_reversed = deque()
        self.aux = deque()
        self.aux_counter = 0  # count of usable items in the aux stacks!

    def push(self, x):
        # push always go to the input stack!
        self._count += 1
        self.input.append(x)

    def pop(self):
        if self._count == 0:
            raise Exception('no items left!')

        self._count -= 1

        # try to serve from the input stack if it's not empty
        if self.input:
            return self.input.pop()

        # okay, can not serve from the input stack, use aux stack
        self.aux_counter -= 1

        return self.aux.pop()

    def _clean_aux(self):
        while self.aux_reversed:
            self.aux_reversed.pop()
        while self.aux:
            self.aux.pop()

    def _refill(self):
        # we exhausted the aux data structures and have to carry the data over
        # from the input stack
        self._clean_aux()

        self.aux_counter = len(self.input)  # all items from input stack are copied!

        # here is the problem -- we have to also have aux populated with the input
        # stack, and order has to be preserved!
        # we can do a trick there -- while populating reversed, temporary populate
        # the aux with reversed order as well, and them reverse stack again to the
        # input stack, and then switch the input stack with the aux!

        while self.input:
            x = self.input.pop()
            self.aux_reversed.append(x)
            self.aux.append(x)  # temporary reversed order!

        # okay, now we multiplexed the source data into two stacks with reversed
        # order, let's restore the original order moving from aux to input
        while self.aux:
            x = self.aux.pop()
            self.input.append(x)

        # the only thing left -- swap the stacks!
        self.input, self.aux = self.aux, self.input

    def pull(self):
        # pull operations has to be performed on a reversed stack

        if self._count == 0:
            raise Exception('no items left!')

        self._count -= 1

        # refill aux stacks if empty
        if self.aux_counter == 0:
            self._refill()

        self.aux_counter -= 1

        # okay, and return the item now
        return self.aux_reversed.pop()

    def count(self):
        return self._count

    def __bool__(self):
        return self.count() > 0


quack = Quack()

quack.push(1)
quack.push(2)
quack.push(3)

assert quack.pull() == 1
assert quack.pop() == 3

quack.push(4)
quack.push(5)

assert quack.pull() == 2
assert quack.pop() == 5
assert quack.pop() == 4

quack.push(6)
quack.push(7)
quack.push(8)

assert quack.pull() == 6
assert quack.pull() == 7
assert quack.pull() == 8

assert not quack
