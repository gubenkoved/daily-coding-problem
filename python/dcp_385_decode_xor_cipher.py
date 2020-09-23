# This problem was asked by Apple.
#
# You are given a hexadecimal-encoded string that has been XOR'd against a single
# char.
#
# Decrypt the message. For example, given the string:
#
# 7a575e5e5d12455d405e561254405d5f1276535b5e4b12715d565b5c551262405d505e575f
# You should be able to decrypt it and get:
#
# Hello world from Daily Coding Problem


from collections import Counter

def decode_hex(hexstring):
    b = bytes.fromhex(hexstring)
    return decode(b)


# such deciphering can be done with frequency analysis given that in English
# it's known that some letters are much more frequent that others -- like 'e', 'o'
# the spaces as well...
# or to make it even more robust we can check against the known dictionary...
def decode(b: bytes) -> str:

    def d(seed):
        xored = bytes([x ^ seed for x in b])
        try:
            decoded = xored.decode(encoding='ascii')
            return decoded
        except:
            return None

    # computes how likely it's that given string is proper English sentence
    def metric(s):
        if s is None:
            return -1
        metric = 0
        for c in s:
            if 'a' <= c.lower() <= 'z':
                metric += 1
                if c.isupper():
                    metric -= 0.1
            elif c == ' ':
                metric += 1

        return metric

    for s in range(256):
        decoded = d(s)
        # print(f'{s} [{chr(s)}] [{metric(decoded)}]: {decoded}')

    decoded, seed = sorted([(d(seed), seed) for seed in range(256)],
                           key=lambda x: metric(x[0]), reverse=True)[0]

    print(f'{b} -> "{decoded}", seed="{chr(seed)}"')
    return decoded


def encode(s: str, seed: int) -> bytes:
    b = s.encode('ascii')
    return bytes([x ^ seed for x in b])


def encode_hex(s: str, seed: int) -> str:
    b = encode(s, seed)
    return b.hex()


assert decode_hex('7a575e5e5d12455d405e561254405d5f1276535b5e4b12715d565b5c551262405d505e575f') == 'Hello world from Daily Coding Problem'
assert decode_hex(encode_hex('Hello, world! Something more...', ord('Q'))) == 'Hello, world! Something more...'
assert decode_hex(encode_hex('test -- single word is not enough', ord('x'))) == 'test -- single word is not enough'
assert decode_hex(encode_hex('the end', ord('x'))) == 'the end'
