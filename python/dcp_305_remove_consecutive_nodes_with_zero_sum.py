# This problem was asked by Amazon.

# Given a linked list, remove all consecutive nodes that
# sum to zero. Print out the remaining nodes.

# For example, suppose you are given the input
# 3 -> 4 -> -7 -> 5 -> -6 -> 6.
# In this case, you should first remove 3 -> 4 -> -7,
# then -6 -> 6, leaving only 5.

class Node(object):

    def __init__(self, val: int, next=None):
        self.next = next
        self.val = val
        pass

    def __str__(self):
        cur = self
        s = ''
        while cur is not None:
            s += f'{cur.val} -> '
            cur = cur.next

        s += 'nil'

        return s


def remove_consecutive_with_zero_sum(head: Node):
    print(f'processing {head}')

    new_head = head
    l = head
    l_prev = None

    while True:
        r = l
        sum = l.val

        while True:
            if sum == 0:
                print(f' .. removing part from {l.val} to {r.val}')
                if l_prev is None:
                    new_head = r.next
                else:
                    l_prev.next = r.next

                l = r.next
                #adjusted = True

            # advance right pointer
            r = r.next

            if r is None:
                break

            sum += r.val

        if l is None or l.next is None:
            break

        l_prev = l
        l = l.next

    print(f'result: {new_head}')
    return new_head

def create_linked_list(values):
    head = None
    prev = None

    for x in values:
        if prev is None:
            prev = Node(x)
            head = prev
        else:
            prev.next = Node(x)
            prev = prev.next

    return head

remove_consecutive_with_zero_sum(create_linked_list([1, -2, 3, -4, 5, -3]))
remove_consecutive_with_zero_sum(create_linked_list([1, 1, 0, -1, 2]))
remove_consecutive_with_zero_sum(create_linked_list([3, 4, -7, 5, -6, 6]))
