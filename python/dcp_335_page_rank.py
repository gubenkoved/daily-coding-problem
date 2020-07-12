# This problem was asked by Google.

# PageRank is an algorithm used by Google to rank the importance of different websites.
# While there have been changes over the years, the central idea is to assign each site
# a score based on the importance of other pages that link to that page.

# More mathematically, suppose there are N sites, and each site i has a certain count Ci
# of outgoing links. Then the score for a particular site Sj is defined as :

# score(Sj) = (1 - d) / N + d * (score(Sx) / Cx+ score(Sy) / Cy+ ... + score(Sz) / Cz))

# Here, Sx, Sy, ..., Sz denote the scores of all the other sites that have outgoing links
# to Sj, and d is a damping factor, usually set to around 0.85, used to model the probability
# that a user will stop searching.

# Given a directed graph of links between various websites, write a program that calculates
# each site's page rank.


from typing import Dict, List


class Node(object):
    def __init__(self, val) -> None:
        self.val = val
        self.neighboors: List[Node] = []

    def add_neighboor(self, node) -> None:
        self.neighboors.append(node)

    def neighboors_count(self) -> int:
        return len(self.neighboors)

    def __str__(self) -> str:
        return self.val

    def __repr__(self) -> str:
        return self.val

class Graph(object):
    def __init__(self, nodes) -> None:
        self.nodes = nodes or []

    def add_node(self, node) -> None:
        self.nodes.append(node)

    def get_referencing(self, target) -> List[Node]:
        result = []
        for node in self.nodes:
            if target in node.neighboors:
                result.append(node)
        return result

def page_rank(graph: Graph) -> Dict[Node, float]:
    n = len(graph.nodes)
    d = 0.85
    start_score = ((1 - d) / n)
    scores = {node: start_score for node in graph.nodes}
    precision = 0.01

    # multiple rounds are needed in order to handle cycles
    round_cntr = 1
    go_on = True
    while go_on:
        # print(f' round #{round_cntr}')

        go_on = False
        round_cntr += 1

        if round_cntr > 100:  # prevent endless loops
            break

        for node in graph.nodes:
            referencing = graph.get_referencing(node)
            new_score = start_score

            for rnode in referencing:
                new_score += d * scores[rnode] / rnode.neighboors_count()

            # print(f'  {node}: {scores[node]} -> {new_score}')
            diff = abs(scores[node] - new_score)

            if diff > precision:
                go_on = True

            scores[node] = new_score

    return scores


a = Node("a")
b = Node("b")
c = Node("c")
d = Node("d")
e = Node("e")

a.add_neighboor(b)
a.add_neighboor(c)
a.add_neighboor(d)
b.add_neighboor(c)
c.add_neighboor(b)  # loop!
d.add_neighboor(c)
e.add_neighboor(a)
e.add_neighboor(c)
e.add_neighboor(d)

graph = Graph([a, b, c, d, e])

print(page_rank(graph))
