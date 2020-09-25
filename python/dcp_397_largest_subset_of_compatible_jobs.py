# This problem was asked by Microsoft.
#
# You are given a list of jobs to be done, where each job is represented by a start
# time and end time. Two jobs are compatible if they don't overlap. Find the largest
# subset of compatible jobs.
#
# For example, given the following jobs (there is no guarantee that jobs will be
# sorted):
#
# [(0, 6),
# (1, 4),
# (3, 5),
# (3, 8),
# (4, 7),
# (5, 9),
# (6, 10),
# (8, 11)]
#
# Return:
#
# [(1, 4),
# (4, 7),
# (8, 11)]


# this sounds like an interpretation of the classic problem where it's required
# to find the biggest amount of classes one can take given the schedule...
# it has a greedy solution

# if we simply sort the jobs by the END time it's quite obvious that we should simply
# prefer the job that ends the earliest among others; it's can simply be shown true
# in this way; suppose we have multiple jobs to pick from -- if we pick the one
# which ends AFTER some other one, then by picking THAT OTHER we never do any worse
# it only adds an additional room for us to pick another job; so by this reasoning
# every time we simply pick non-overlapping range that ENDS the earliest;

# equivalent one (solution worked)
# https://leetcode.com/problems/non-overlapping-intervals/

def f(jobs):
    result = []
    last_end = None
    for job in sorted(jobs, key=lambda t: t[1]):
        start, end = job
        # take the job if it starts AFTER the last one ended
        if last_end is None or start >= last_end:
            last_end = end
            result.append(job)
    print(result)
    return result


assert (f([(0, 6), (1, 4), (3, 5), (3, 8), (4, 7), (5, 9), (6, 10), (8, 11)])
        == [(1, 4), (4, 7), (8, 11)])
