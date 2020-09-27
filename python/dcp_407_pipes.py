# This problem was asked by Samsung.
#
# A group of houses is connected to the main water plant by means of a set of pipes.
# A house can either be connected by a set of pipes extending directly to the plant,
# or indirectly by a pipe to a nearby house which is otherwise connected.
#
# For example, here is a possible configuration, where A, B, and C are houses, and
# arrows represent pipes:
#
# A <--> B <--> C <--> plant
#
# Each pipe has an associated cost, which the utility company would like to minimize.
# Given an undirected graph of pipe connections, return the lowest cost configuration
# of pipes such that each house has access to water.
#
# In the following setup, for example, we can remove all but the pipes from plant to
# A, plant to B, and B to C, for a total cost of 16.
#
# pipes = {
#     'plant': {'A': 1, 'B': 5, 'C': 20},
#     'A': {'C': 15},
#     'B': {'C': 10},
#     'C': {}
# }


# okay, DCP is officially broken -- this one is exact copy of DCP 299
# this problem simply maps to the task solved by Kruskal algorithm in a greedy way
# let's use disjoint set data structure to make it funnier and more efficient;
# may be solving the same tasks is useful too as it is a chance to see an improvement
# PS. Looks like the solution I came up with back then is EXACTLY the same (but in C#)

from typing import List
from collections import namedtuple

Edge = namedtuple('Edge', ['first', 'second', 'cost'])


def min_spanning_tree(adjacency_list) -> List[Edge]:
    # disjoint set is simply a mapping of the element to a parent one
    # forming groups, "root" elements have themselves as parents
    nodes = adjacency_list.keys()
    disjoint_set = {key: key for key in nodes}

    def ultimate_parent(node):
        parent = disjoint_set[node]
        while disjoint_set[parent] != parent:
            parent = disjoint_set[parent]
        return parent

    def union(a, b):
        disjoint_set[b] = a

    # Kruskal algorithms suggests to sort all the edges by the cost and start adding
    # edges; edge should be skipped it it connects already connected components of
    # connectedness
    edges: List[Edge] = []

    for node in adjacency_list:
        for adjacent in adjacency_list[node]:
            cost = adjacency_list[node][adjacent]
            edges.append(Edge(node, adjacent, cost))

    # sort edges, cheapest first!
    edges = sorted(edges, key=lambda edge: edge.cost)

    result = []

    for edge in edges:
        from_parent = ultimate_parent(edge.first)
        to_parent = ultimate_parent(edge.second)

        if from_parent == to_parent:
            continue  # already connected!

        result.append(edge)
        union(from_parent, to_parent)

    print(result)

    return result


pipes = {
    'plant': {'A': 1, 'B': 5, 'C': 20},
    'A': {'C': 15},
    'B': {'C': 10},
    'C': {}
}

assert sum(edge.cost for edge in min_spanning_tree(pipes)) == 16
