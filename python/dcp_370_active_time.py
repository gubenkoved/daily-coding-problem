# This problem was asked by Postmates.
#
# The “active time” of a courier is the time between the pickup and dropoff of a
# delivery. Given a set of data formatted like the following:
#
# (delivery id, timestamp, pickup/dropoff)
# Calculate the total active time in seconds. A courier can pick up multiple orders
# before dropping them off. The timestamp is in unix epoch seconds.
#
# For example, if the input is the following:
#
# (1, 1573280047, 'pickup')
# (1, 1570320725, 'dropoff')
# (2, 1570321092, 'pickup')
# (3, 1570321212, 'pickup')
# (3, 1570322352, 'dropoff')
# (2, 1570323012, 'dropoff')
#
# The total active time would be 1260 seconds.


# the problem statement seems to be incorrect, the first time is way to high

def t(data):
    data = sorted(data, key=lambda t: t[1])  # by time asc
    m = {}
    result = 0
    for id, time, action in data:
        if action == 'pickup':
            m[id] = time
        else:
            result += time - m[id]
    return result

data = [
    (1, 1570320625, 'pickup'),  # changed to dropoff -100s
    (1, 1570320725, 'dropoff'),
    (2, 1570321092, 'pickup'),
    (3, 1570321212, 'pickup'),
    (3, 1570322352, 'dropoff'),
    (2, 1570323012, 'dropoff'),
]

assert t(data) == 3160