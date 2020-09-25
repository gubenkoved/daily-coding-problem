# This problem was asked by Amazon.
#
# Given a linked list and an integer k, remove the k-th node from the end of the list
# and return the head of the list.
#
# k is guaranteed to be smaller than the length of the list.
#
# Do this in one pass.

class Node:
    def __init__(self, val, next=None):
        self.val = val
        self.next = next

    def __str__(self):
        return f'{self.val} -> {self.next}'


def remove_k_last(head, k):
    # we need to pointers where one is lagging behind another one on exactly k nodes
    # then we iterate with both pointers
    # hm... does it qualify for "one pass"?
    slow = head  # will point to the element right BEFORE the one to be deleted
    fast = head

    for _ in range(k):
        fast = fast.next

    # remove the head! so cruel...
    if fast is None:
        return head.next

    while fast.next is not None:
        fast = fast.next
        slow = slow.next

    # regular case... simply skip one node
    slow.next = slow.next.next
    return head


head = Node(1, Node(2, Node(3, Node(4, Node(5, Node(6))))))

head = remove_k_last(head, 3)
assert str(head) == '1 -> 2 -> 3 -> 5 -> 6 -> None'
head = remove_k_last(head, 1)
assert str(head) == '1 -> 2 -> 3 -> 5 -> None'
head = remove_k_last(head, 4)
assert str(head) == '2 -> 3 -> 5 -> None'
