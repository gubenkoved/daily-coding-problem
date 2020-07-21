# This problem was asked by Airbnb.

# You are given a huge list of airline ticket prices between different cities
# around the world on a given day. These are all direct flights. Each element
# in the list has the format (source_city, destination, price).

# Consider a user who is willing to take up to k connections from their origin
# city A to their destination B. Find the cheapest fare possible for this
# journey and print the itinerary for that journey.

# For example, our traveler wants to go from JFK to LAX with up to 3
# connections, and our input flights are as follows:

# [
#     ('JFK', 'ATL', 150),
#     ('ATL', 'SFO', 400),
#     ('ORD', 'LAX', 200),
#     ('LAX', 'DFW', 80),
#     ('JFK', 'HKG', 800),
#     ('ATL', 'ORD', 90),
#     ('JFK', 'LAX', 500),
# ]

# Due to some improbably low flight prices, the cheapest itinerary would be
# JFK -> ATL -> ORD -> LAX, costing $440.


from typing import List, Tuple

Itinerary = List[str]

def cheapest(from_location: str, to_location: str, flights: Tuple[str, str, float], max_connections: int) -> Itinerary:

    results: List[Tuple[Itinerary, float]] = []

    def get_adjacent(from_location: str) -> List[Tuple[str, float]]:
        results = []
        for f in flights:
            if f[0] == from_location:
                results.append((f[1], f[2]))
        return results

    def search(cur_itinerary: Itinerary, connections_left: int, price: float) -> None:
        nonlocal results

        cur_location = cur_itinerary[-1]
        if cur_location == to_location:
            results.append((list(cur_itinerary), price))
            return

        if connections_left == 0:
            return

        for adjacent in get_adjacent(cur_location):
            if adjacent[0] in cur_itinerary:
                continue  # avoid cycles!

            next_node = adjacent[0]
            next_price = adjacent[1]

            # add to the itinerary
            cur_itinerary.append(next_node)
            search(cur_itinerary, connections_left - 1, price + next_price)
            del cur_itinerary[-1]

    search([from_location], max_connections, 0)

    if len(results) == 0:
        return None

    result = sorted(results, key=lambda x: x[1])[0]

    return result


flights = [
    ('JFK', 'ATL', 150),
    ('ATL', 'SFO', 400),
    ('ORD', 'LAX', 200),
    ('LAX', 'DFW', 80),
    ('JFK', 'HKG', 800),
    ('ATL', 'ORD', 90),
    ('JFK', 'LAX', 500),
    ('ATL', 'JFK', 1),  # cycle!
]

print(cheapest('JFK', 'LAX', flights, 3))
print(cheapest('JFK', 'LAX', flights, 0))
