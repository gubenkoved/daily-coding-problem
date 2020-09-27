# This problem was asked by Facebook.
#
# Given an array of numbers representing the stock prices of a company in
# chronological order and an integer k, return the maximum profit you can make
# from k buys and sells. You must buy the stock before you can sell it, and you must
# sell the stock before you can buy it again.
#
# For example, given k = 2 and the array [5, 2, 4, 0, 1], you should return 3.

# == DCP 130 :(
# Dynamic Programming problem
def max_profit(prices, k):
    # d[k][i] is max profit from exactly k deals selling at i-th day
    # d[0] -- unused, for clarity
    d = [[None for _ in prices] for _ in range(k+1)]

    n = len(prices)

    # base case for the first deal
    min_price_left = prices[0]
    for sell_at in range(1, n):
        d[1][sell_at] = prices[sell_at] - min_price_left
        min_price_left = min(min_price_left, prices[sell_at])

    # okay, now all other cases with different deals count can be expressed via
    # solutions for the previous deals count
    for deal_num in range(2, k+1):
        for sell_at in range(1, n):
            max_profit = None
            # previous day sell should finish at least 2 days earlier so that
            # we could have bought the stock next day again
            for prev_sell_at in range(1, sell_at-1):
                if d[deal_num - 1][prev_sell_at] is None:
                    continue  # it was impossible to do a sell at this day
                min_price_for_current_deal = min(prices[prev_sell_at+1:sell_at])
                profit = (d[deal_num - 1][prev_sell_at]
                          + prices[sell_at]
                          - min_price_for_current_deal)
                if max_profit is None or profit > max_profit:
                    max_profit = profit
            d[deal_num][sell_at] = max_profit

    print('DP GRID:')
    for row in d[1:]:
        print(f'\t{row}')

    # to find the answer simply find the max profit for k deals
    return max([profit for profit in d[k] if profit is not None])


assert max_profit([5, 2, 4, 0, 1], 1) == 2
assert max_profit([5, 2, 4, 0, 1], 2) == 3
assert max_profit([1, 2, 3, 4, 5, 6, 7, 8, 9, 10], 3) == 7
assert max_profit([1, 2, 3, 4, 5, 0, 7, 1, 9, 10], 3) == 4 + 7 + 9
