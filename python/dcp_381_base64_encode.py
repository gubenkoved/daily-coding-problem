# This problem was asked by Paypal.
#
# Read this Wikipedia article on Base64 encoding:
# https://en.wikipedia.org/wiki/Base64
#
# Implement a function that converts a hex string to base64.
#
# For example, the string:
#
# deadbeef
# should produce:
#
# 3q2+7w==


def base64_encode_hex(hex_string):
    return base64_encode(bytes.fromhex(hex_string))


def base64_encode(b: bytes):
    # base64 converts 6 bits into 1 ascii char,
    # so let's operate with 3 bytes (3 * 8 = 24 bits) as it maps into 4 chars nicely
    # and the process can repeat itself
    def _m(x):
        # assuming we map least 6 bits
        if x < 0:
            raise Exception(f'unmappable: {x}')

        if x <= 25:
            return chr(ord('A') + x)
        elif x <= 51:
            return chr(ord('a') + x - 26)
        elif x <= 61:
            return chr(ord('0') + x - 52)
        elif x == 62:
            return '+'
        elif x == 63:
            return '/'

        raise Exception(f'unmappable: {x}')

    n = len(b)
    padding = 0
    result = ''

    idx = 0

    while idx < n:
        window = b[idx:min(idx+3, n)]

        if len(window) != 3:
            padding = 3 - len(window)

        # convert window into the ascii
        if len(window) == 1:
            # 2 chars result
            b1 = window[0]
            result += _m(b1 >> 2)
            result += _m((b1 & 0b11) << 4)
        elif len(window) == 2:
            # 3 chars result
            b1, b2 = window[0], window[1]
            result += _m(b1 >> 2)
            result += _m(((b1 & 0b11) << 4) | (b2 >> 4))
            result += _m((b2 & 0b1111) << 2)
        elif len(window) == 3:
            # 4 chars result
            b1, b2, b3 = window[0], window[1], window[2]
            result += _m(b1 >> 2)
            result += _m(((b1 & 0b11) << 4) | (b2 >> 4))
            result += _m(((b2 & 0b1111) << 2) | (b3 >> 6))
            result += _m(b3 & 0b111111)
        else:
            raise Exception(f'unexpected window size: {len(window)}')

        # move to the next bytes triplet
        idx += 3

    result = result + ('=' * padding)
    return result


assert base64_encode_hex('00') == 'AA=='
assert base64_encode_hex('0000') == 'AAA='
assert base64_encode_hex('000000') == 'AAAA'
assert base64_encode_hex('deadbe') == '3q2+'
assert base64_encode_hex('deadbeef') == '3q2+7w=='
assert base64_encode_hex('68656c6c6f2c20776f726c64') == 'aGVsbG8sIHdvcmxk'  # "hello, word" in UTF-8
