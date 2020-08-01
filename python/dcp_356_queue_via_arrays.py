# This problem was asked by Netflix.

# Implement a queue using a set of fixed-length arrays.

# The queue should support enqueue, dequeue, and get_size operations.


class Queue(object):
    def __init__(self, buckets_count: int = 3, bucket_size: int = 3) -> None:
        self.__buckets_count = buckets_count
        self.__bucket_size = bucket_size
        self.__buckets = [[None for _ in range(bucket_size)]
                          for _ in range(buckets_count)]
        self.__end = (0, 0)  # idx, offset
        self.__start = (0, 0)

    def __next(self, pos):
        idx, offset = pos

        if offset == self.__bucket_size - 1:
            idx += 1
            offset = 0
        else:
            offset += 1

        if idx == self.__bucket_size:
            idx = 0

        return (idx, offset)

    def queue(self, item):
        next_pos = self.__next(self.__end)

        if self.__start == next_pos:
            raise Exception('no space left!')

        bucket_idx = self.__end[0]
        offset = self.__end[1]
        bucket = self.__buckets[bucket_idx]
        bucket[offset] = item

        # advance the pointers
        self.__end = next_pos

    def dequeue(self):
        if self.__start == self.__end:
            raise Exception('no items!')

        bucket_idx = self.__start[0]
        offset = self.__start[1]
        bucket = self.__buckets[bucket_idx]

        # advance the pointers
        self.__start = self.__next(self.__start)

        return bucket[offset]

    def get_size(self):
        buckets_diff = self.__end[0] - self.__start[0]

        if buckets_diff < 0:
            buckets_diff += self.__buckets_count

        return self.__bucket_size * buckets_diff \
            - self.__start[1] + self.__end[1]


# tests
q = Queue(buckets_count=3, bucket_size=3)

for i in range(5):
    print(f'adding {i}')
    q.queue(i)

print(q.dequeue())
print(q.dequeue())
print(q.dequeue())

for i in range(5):
    print(f'adding {i}')
    q.queue(i)

while q.get_size() > 0:
    print(q.dequeue())

q.queue(1)
q.queue(2)
q.queue(3)

assert(q.get_size() == 3)