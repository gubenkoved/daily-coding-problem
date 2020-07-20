# This problem was asked by Google.

# You are given a set of synonyms, such as (big, large) and (eat, consume).
# Using this set, determine if two sentences with the same number of words are equivalent.

# For example, the following two sentences are equivalent:

# "He wants to eat food."
# "He wants to consume food."

# Note that the synonyms (a, b) and (a, c) do not necessarily imply (b, c): consider
# the case of (coach, bus) and (coach, teacher).

# Follow-up: what if we can assume that (a, b) and (a, c) do in fact imply (b, c)?

def equivalent(sentence1, sentence2, synonyms) -> bool:

    def normalize(s: str) -> str:
        words = s.split(' ')
        normalized = ' '.join([normalize_word(w) for w in words])

        print(f' {s} -> {normalized}')

        return normalized

    def normalize_word(s: str) -> str:
        nonlocal synonyms

        for synonyms_pair in synonyms:
            if s in synonyms_pair:
                return synonyms_pair[0]

        return s

    return normalize(sentence1) == normalize(sentence2)


def equivalent_transitive(sentence1, sentence2, synonyms) -> bool:
    disjoint_set = {}

    for synonyms_pair in synonyms:
        if synonyms_pair[0] in disjoint_set:
            disjoint_set[synonyms_pair[1]] = disjoint_set[synonyms_pair[0]]
        elif synonyms_pair[1] in disjoint_set:
            disjoint_set[synonyms_pair[0]] = disjoint_set[synonyms_pair[1]]
        else:
            disjoint_set[synonyms_pair[1]] = synonyms_pair[0]

    def normalize_word(s: str) -> str:
        nonlocal disjoint_set

        if s not in disjoint_set:
            return s

        normalized = disjoint_set[s]

        while normalized in disjoint_set:
            normalized = disjoint_set[normalized]

        return normalized

    print(disjoint_set)

    def normalize(s: str) -> str:
        words = s.split(' ')
        normalized = ' '.join([normalize_word(w) for w in words])

        print(f' {s} -> {normalized}')

        return normalized

    return normalize(sentence1) == normalize(sentence2)


assert equivalent('He wants to eat food.', 'He wants to consume food.', [('eat', 'consume'), ('big', 'large')])
assert not equivalent('He wants to eat food.', 'He wants to eliminate food.', [('eat', 'consume'), ('big', 'large'), ('consume', 'eliminate')])
assert not equivalent_transitive('He wants to eat food.', 'He wants to eliminate food.', [('eat', 'consume')])
assert equivalent_transitive('He wants to eat food.', 'He wants to eliminate food.', [('eat', 'consume'), ('consume', 'eliminate')])
