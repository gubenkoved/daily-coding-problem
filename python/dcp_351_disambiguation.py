# This problem was asked by Quora.

# Word sense disambiguation is the problem of determining which sense a word takes on
# in a particular setting, if that word has multiple meanings. For example, in the
# sentence "I went to get money from the bank", bank probably means the place where
# people deposit money, not the land beside a river or lake.

# Suppose you are given a list of meanings for several words, formatted like so:

# {
#     "word_1": ["meaning one", "meaning two", ...],
#     ...
#     "word_n": ["meaning one", "meaning two", ...]
# }

# Given a sentence, most of whose words are contained in the meaning list above,
# create an algorithm that determines the likely sense of each possibly ambiguous
# word.


# had to google what the heck is that to find an algorithm that makes sense:
# https://en.wikipedia.org/wiki/Lesk_algorithm


from typing import Dict, List, Tuple
import re


def normalize(word: str) -> str:
    # in order to look the words up in the dictionary we have to be able to
    # normalize words into the form which is present in the dictionary

    if word[-1] == 's':
        return word[:-1]

    return word

def split_into_words(s: str) -> List[str]:
    words = re.split(r'\s', s)
    return words

def is_stop_word(s: str) -> bool:
    return s in ['a', 'the', 'is', 'of', 'with']  # etc..

def intersection_rate_word(word1: str, word2: str):
    if normalize(word1) == normalize(word2):
        return 1

    return 0

def intersection_rate(context: List[str], meaning: str) -> float:
    meaning_words = split_into_words(meaning)

    result = 0

    for context_word in context:
        for meaning_word in meaning_words:
            if is_stop_word(context_word) or is_stop_word(meaning_word):
                continue

            result += intersection_rate_word(context_word, meaning_word)

    return result

def resolve(sentence: str, dic: Dict[str, str]) -> List[Tuple[int, str, str]]:  # Tuple: word index, original word, meaning chosen
    # basically Lesk algorithm suggest to take a context (k surrounding word from each side) of the given ambiguous word
    # and try to match it against each of the definitions in the dictionary -- the definition with the biggest intersection
    # rate wins

    words = split_into_words(sentence)
    resolved = []
    k = 5

    for idx, word in enumerate(words):
        nword = normalize(word)
        context = set()

        for ctx_idx in range(idx - k, idx + k + 1):
            if ctx_idx >= 0 and ctx_idx < len(words):
                context.add(words[ctx_idx])

        if nword in dic and len(dic[nword]) > 1:
            # pick the best meaning
            weighted_meanings = [(meaning, intersection_rate(context, meaning)) for meaning in dic[nword]]
            best_meaning = sorted(weighted_meanings, key=lambda kvp: kvp[1], reverse=True)[0]
            resolved.append((idx, word, best_meaning))

    return resolved


dic = {
    'pine': [
        'kinds of evergreen tree with needle-shaped leaves',
        'waste away through sorrow or illness',
    ],
    'cone': [
        'solid body which narrows to a point',
        'something of this shape whether solid or hollow',
        'fruit of certain evergreen trees'
    ]
}


print(resolve('John found the best pine cone that a tree can produce', dic))
print(resolve('John has hit by some solid cone', dic))
