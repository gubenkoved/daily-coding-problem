# This problem was asked by Apple.

# Given a linked list, uniformly shuffle the nodes. What if we want to prioritize space over time?


import itertools
from random import randint

class Node(object):
    def __init__(self, val, next=None) -> None:
        self.value = val
        self.next = next

def insert(root: Node, index: int, value) -> Node:
    node = Node(value)

    cur_idx = 0
    cur = root
    prev = None

    while True:

        if cur_idx == index:
            # insert BEFORE current
            node.next = cur

            if prev is not None:
                prev.next = node
            else:  # head is changed
                root = node

            break  # over!

        if cur is None:
            raise Exception(f'unable to insert item at {index} as we only found {cur_idx} items')

        prev = cur
        cur = cur.next
        cur_idx += 1

    return root

def shuffle(root: Node) -> Node:
    # iterate over the source list and maintain a new linked list
    # and every time we randomly choose the position of the new node

    source_ptr: Node = root.next
    target_head: Node = Node(root.value)
    target_len = 1

    while source_ptr is not None:
        new_index = randint(0, target_len)
        target_head = insert(target_head, new_index, source_ptr.value)  # head can change
        source_ptr = source_ptr.next
        target_len += 1

    return target_head

def generate(n: int) -> Node:
    root = None
    prev = None

    for i in range(n):
        # node = Node(randint(0, 1000))
        node = Node(i)

        if prev:
            prev.next = node
        else:
            root = node
            prev = node

        prev = node

    return root


def list_values(root: Node):
    cur = root
    values = []

    while cur is not None:
        values.append(cur.value)
        cur = cur.next

    return values

def print_list(root):
    print(' -> '.join(map(str, list_values(root))))


def check_distributions(n, times):
    freq_map = {}  # map NUMBER -> positions list

    for _ in range(times):
        vals = list_values(shuffle(generate(n)))

        for val_idx in range(len(vals)):
            val = vals[val_idx]
            if val not in freq_map:
                freq_map[val] = []
            freq_map[val].append(val_idx)

    for source in sorted(freq_map.keys()):
        groupped = itertools.groupby(sorted(freq_map[source]))
        print([len(list(group[1])) for group in groupped])


print_list(shuffle(generate(30)))
check_distributions(10, 10000)
