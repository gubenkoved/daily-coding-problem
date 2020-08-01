# This problem was asked by Dropbox.

# Create a data structure that performs all the following operations
# in O(1) time:

# plus: Add a key with value 1. If the key already exists, increment its
# value by one.
# minus: Decrement the value of a key. If the key's value is currently 1,
# remove it.
# get_max: Return a key with the highest value.
# get_min: Return a key with the lowest value.

from typing import Optional


# we need the linked list in order to efficiently track the min/max...
# and regular Python's dequeue is not flexible enough to get this one solved
class Node:
    def __init__(self, val):
        self.val = val
        self.prev = None
        self.next = None


class LinkedList:
    def __init__(self) -> None:
        self.first = None
        self.last = None

    def peek_first(self) -> Optional[Node]:
        return self.first

    def peek_last(self) -> Optional[Node]:
        return self.last

    def add_last(self, value) -> Node:
        node = Node(value)

        # rewire pointers
        if self.last is not None:
            self.last.next = node
            node.prev = self.last
            self.last = node
        else:
            self.last = node

        # edge case -- adding the very first item to the list
        if self.first is None:
            self.first = node

        return node

    def move_left(self, node) -> None:
        prev = node.prev
        next = node.next

        if prev is None:
            raise Exception('this node has no items to the left!')

        node.prev = prev.prev
        node.next = prev

        if node.prev is None:  # got the new left-most one
            self.first = node

        prev.next = next
        prev.prev = node

        if next is not None:
            next.prev = prev
        else:  # moving the last one!
            self.last = prev

    def move_right(self, node) -> None:
        prev = node.prev
        next = node.next

        if next is None:
            raise Exception('this node has no items to the right!')

        node.next = next.next
        node.prev = next

        if node.next is None:  # got the new right-most one
            self.last = node

        next.prev = prev
        next.next = node

        if prev is not None:
            prev.next = next
        else:  # moving the first one!
            self.first = next

    def remove_node(self, node) -> None:
        if node.prev is not None:
            node.prev.next = node.next
        else:
            self.first = node.next

        if node.next is not None:
            node.next.prev = node.prev
        else:
            self.last = node.prev


class Counter(object):
    def __init__(self) -> None:
        self.map = {}
        self.ordered = LinkedList()  # contains ordered linked list of keys by value

    def plus(self, key):
        if key not in self.map:
            node = self.ordered.add_last((key, 1))
            self.map[key] = node
        else:
            node = self.map[key]
            node.val = (key, node.val[1] + 1)

            while node.prev is not None and node.prev.val[1] < node.val[1]:
                self.ordered.move_left(node)

    def minus(self, key):
        if key not in self.map:
            raise Exception('item was not found')

        node = self.map[key]

        if node.val[1] == 1:
            del self.map[key]
            self.ordered.remove_node(node)
        else:
            node.val = (key, node.val[1] - 1)

            while node.next is not None and node.next.val[1] > node.val[1]:
                self.ordered.move_right(node)

    def get_max(self):
        key, count = self.ordered.peek_first().val
        return key

    def get_min(self):
        key, count = self.ordered.peek_last().val
        return key


c = Counter()

c.plus('a')
c.plus('b')
c.plus('b')
c.plus('b')
c.plus('c')
c.plus('c')

assert(c.get_max() == 'b')
assert(c.get_min() == 'a')

c.plus('a')
c.plus('a')
c.plus('a')

assert(c.get_max() == 'a')
assert(c.get_min() == 'c')
