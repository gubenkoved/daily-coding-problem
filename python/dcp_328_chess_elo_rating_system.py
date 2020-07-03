# This problem was asked by Facebook.

# In chess, the Elo rating system is used to calculate player
# strengths based on game results.

# A simplified description of the Elo system is as follows. Every player
# begins at the same score. For each subsequent game, the loser transfers
# some points to the winner, where the amount of points transferred depends
# on how unlikely the win is. For example, a 1200-ranked player should gain
# much more points for beating a 2000-ranked player than for beating a 1300-ranked player.

# Implement this system.

class RatingSystem(object):
    def __init__(self):
        self.ratings_map = {}  # 'player-name' -> rating
        self.start_rating = 1000
        self.alpha = 0.1  # the fraction of the difference between ratings to be tranfered per game
        self.min_rating_change = 5
        self.stornger_wins_max_change = 40
        self.stronger_wins_rating_diff_to_min_reward = 400  # if rating diff between players more than that winner gets the min rating change

    def add_player(self, player_name: str, start_rating: None) -> None:
        self.ratings_map[player_name] = start_rating or self.start_rating

    def handle_results(self, winner_player: str, looser_player: str) -> None:
        diff = abs(self.ratings_map[winner_player] - self.ratings_map[looser_player])
        weaker_win_price = max(self.min_rating_change, diff * self.alpha)  # when looser wins we transfer alpha * rating diff to the looser
        stronger_win_price = max(self.min_rating_change, self.stornger_wins_max_change * (1 - diff / self.stronger_wins_rating_diff_to_min_reward))

        if self.ratings_map[winner_player] >= self.ratings_map[looser_player]:
            price = stronger_win_price
        else:
            price = weaker_win_price

        price = int(price)

        print(f'{looser_player} ({self.ratings_map[looser_player]}) -> {winner_player} ({self.ratings_map[winner_player]}) {price} ELO')

        self.ratings_map[winner_player] += price
        self.ratings_map[looser_player] -= price


s = RatingSystem()

s.add_player('kasparov', 2000)
s.add_player('ivanov', 1000)
s.add_player('petrov', 1200)
s.add_player('sidorov', 1500)

s.handle_results('kasparov', 'ivanov')
s.handle_results('petrov', 'kasparov')
s.handle_results('petrov', 'kasparov')
s.handle_results('petrov', 'kasparov')
s.handle_results('petrov', 'kasparov')
s.handle_results('petrov', 'kasparov')
s.handle_results('kasparov', 'petrov')

print(s.ratings_map)
