# This problem was asked by Jane Street.

# The United States uses the imperial system of weights and measures, which means that there
# are many different, seemingly arbitrary units to measure distance. There are 12 inches in
# a foot, 3 feet in a yard, 22 yards in a chain, and so on.

# Create a data structure that can efficiently convert a certain quantity of one unit to the
# correct amount of any other unit. You should also allow for additional units to be added
# to the system.


class Converter(object):
    def __init__(self):
        self.map = {}  # convertion rates map (from, to) -> rate which means from -> to x rate
        self.units = set()

    def add_unit(self, unit_name: str, rate: float = None, another_unit_name: str = None) -> None:
        if another_unit_name is None and len(self.map) != 0:
            raise Exception('unable to add a unit w/o binding to another unit (except the first one)')

        if another_unit_name is not None:
            if another_unit_name not in self.units:
                raise Exception('unknown another unit name')

            # add convertion between 'unit and 'another unit'
            self.map[(unit_name, another_unit_name)] = rate
            self.map[(another_unit_name, unit_name)] = 1 / rate  # backward conversion

            # add all conversion rates to other units via the 'another unit'

            for known_unit in self.units:
                if known_unit == another_unit_name:
                    continue  # skip the one we already handled

                sub_rate = self.convert(another_unit_name, known_unit)

                self.map[(unit_name, known_unit)] = rate * sub_rate
                self.map[(known_unit, unit_name)] = (1 / sub_rate) * (1 / rate)

        self.units.add(unit_name)  # mark the current one as known

    # O(1)
    def convert(self, from_unit_name: str, to_unit_name: str, amount: float = 1) -> float:
        if from_unit_name not in self.units:
            raise Exception(f'unknown unit name: {from_unit_name}')

        if to_unit_name not in self.units:
            raise Exception(f'unknown unit name: {to_unit_name}')

        rate = self.map[(from_unit_name, to_unit_name)]

        return amount * rate


# tests
c = Converter()

c.add_unit('inch')
c.add_unit('foot', 12, 'inch')
c.add_unit('yard', 3, 'foot')
c.add_unit('chain', 22, 'yard')

print(c.convert('chain', 'inch', 1))  # how many inches in the chain?
