# This problem was asked by Google.
#
# Yesterday you implemented a function that encodes a hexadecimal string into Base64.
#
# Write a function to decode a Base64 string back to a hexadecimal string.
#
# For example, the following string:
#
# 3q2+7w==
# should produce:
#
# deadbeef


def base64_decode_hex(s):
    b = base64_decode(s)
    return b.hex()


def base64_decode(s) -> bytes:
    # base64 converts 6 bits into 1 ascii char,
    # so let's operate with 3 bytes (3 * 8 = 24 bits) as it maps into 4 chars nicely
    # and the process can repeat itself

    # maps char into the int where first 6 bits are populated based on char mapping
    def _m(x):
        if 'A' <= x <= 'Z':
            return ord(x) - ord('A')
        elif 'a' <= x <= 'z':
            return ord(x) - ord('a') + 26
        elif '0' <= x <= '9':
            return ord(x) - ord('0') + 52
        elif x == '+':
            return 62
        elif x == '/':
            return 63

        raise Exception(f'unmappable: {x}')

    if len(s) % 4 != 0:
        raise Exception('broken encoded string!')

    n = len(s)
    idx = 0
    b = []

    while idx < n:
        window = s[idx:idx+4]
        padding = len(window) - len(window.rstrip('='))
        k = 4 - padding  # amount of chars to be mapped

        # 4 chars -> 3 bytes
        # 3 chars + '=' -> 2 bytes
        # 2 chars + '==' -> 1 byte
        # 1 char + '===' -- impossible, broken encoding, even 1 byte requires 2 chars!
        if k == 1:
            raise Exception('broken')
        elif k == 2:
            c1, c2 = window[0], window[1]
            b.append(_m(c1) << 2 | _m(c2) >> 4)
        elif k == 3:
            c1, c2, c3 = window[0], window[1], window[2]
            b.append(_m(c1) << 2 | _m(c2) >> 4)
            b.append((_m(c2) & 0b1111) << 4 | _m(c3) >> 2)
        else:
            c1, c2, c3, c4 = window[0], window[1], window[2], window[3]
            b.append(_m(c1) << 2 | _m(c2) >> 4)
            b.append((_m(c2) & 0b1111) << 4 | _m(c3) >> 2)
            b.append((_m(c3) & 0b11) << 6 | _m(c4))

        # move to the next window
        idx += 4

    return bytes(b)


assert base64_decode_hex('AA==') == '00'
assert base64_decode_hex('AAA=') == '0000'
assert base64_decode_hex('AAAA') == '000000'
assert base64_decode_hex('3q2+') == 'deadbe'
assert base64_decode_hex('3q2+7w==') == 'deadbeef'
assert base64_decode_hex('aGVsbG8sIHdvcmxk') == '68656c6c6f2c20776f726c64'
