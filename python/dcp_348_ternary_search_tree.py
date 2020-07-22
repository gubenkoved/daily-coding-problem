# This problem was asked by Zillow.

# A ternary search tree is a trie-like data structure where each node may have up to
# three children. Here is an example which represents the words code, cob, be, ax, war, and we.

#        c
#     /  |  \
#    b   o   w
#  / |   |   |
# a  e   d   a
# |    / |   | \
# x   b  e   r  e

# The tree is structured according to the following rules:

# left child nodes link to words lexicographically earlier than the parent prefix
# right child nodes link to words lexicographically later than the parent prefix
# middle child nodes continue the current word
# For instance, since code is the first word inserted in the tree, and cob lexicographically
# precedes cod, cob is represented as a left child extending from cod.

# Implement insertion and search functions for a ternary search tree.


class TNode(object):
    def __init__(self, val, left=None, middle=None, right=None) -> None:
        self.val = val
        self.left = left
        self.middle = middle
        self.right = right
        self.eos = False  # end of string to handle prefix-equals words

    def __repr__(self):
        if not self.left and not self.middle and not self.right:
            return f'[{self.val}]'

        return f'[{self.val}, {self.left}, {self.middle}, {self.right}, {self.eos}]'

def seed(word: str) -> TNode:
    root = TNode(word[0])
    insert(word, root)
    return root

def insert(word: str, cur: TNode):
    if word[0] == cur.val:
        if len(word) != 1:
            if cur.middle is None:
                cur.middle = TNode(word[1])
            insert(word[1:], cur.middle)
        else:
            cur.eos = True
    elif word[0] < cur.val:
        if cur.left is None:
            cur.left = TNode(word[0])
        insert(word, cur.left)
    else:
        if cur.right is None:
            cur.right = TNode(word[0])
        insert(word, cur.right)


def search(word: str, cur: TNode) -> bool:
    if cur is None:
        return False

    if word[0] == cur.val:
        if len(word) == 1:  # matched last char!
            return cur.eos

        return search(word[1:], cur.middle)
    elif word[0] < cur.val:
        return search(word, cur.left)
    else:
        return search(word, cur.right)


root = seed("code")

print(root)

insert("cob", root)
insert("be", root)
insert("ax", root)
insert("war", root)
insert("we", root)
insert("car", root)
insert("bar", root)
insert("cod", root)  # same prefix as "code" -- will make use of "eos" marker

print(root)

assert search('code', root)
assert search('cod', root)
assert search('war', root)
assert search('be', root)
assert search('bar', root)
assert not search('cad', root)
assert not search('co', root)
assert not search('c', root)
