# This problem was asked by Amazon.
#
# Given a string, find the length of the smallest window that contains every distinct character. Characters may
# appear more than once in the window.
#
# For example, given "jiujitsu", you should return 5, corresponding to the final five letters.


def min_window(s: str) -> str:
    # algorithm
    # 1. set left and right pointers to the first character
    # 2. grow window (via right pointer) until it has ALL the chars
    #   a. if window can contain ALL chars via moving right pointer to the right -> FINISH
    # 3. contract window (via left pointer) while it still has ALL chars
    # 4. store the window if it's less than the running best window
    # 5. move left pointer 1 char to the right causing window to miss 1 char

    l: int = 0  # left pointer for the window (included)
    r: int = 0  # right pointer for the window (excluded)

    window_char_map = {}  # char -> count mapping for the window
    best_window: str = s

    def grow_while_missing_chars():
        nonlocal r
        missing = set(s)

        while len(missing) > 0:
            c = s[r]

            if c in missing:
                missing.remove(c)
                window_char_map[c] = 0

            window_char_map[c] += 1
            r += 1

    # grows the window until hit target char
    # returns True if successful, otherwise False if we were unable to find an occurrence
    def grow_until(target_char: str) -> bool:
        nonlocal r
        while r < len(s):
            # add to the window
            c = s[r]
            window_char_map[c] += 1
            r += 1

            if c == target_char:
                return True  # stop if we hit the target char

        return False

    def contract_while_excess() -> None:
        nonlocal l

        while window_char_map[s[l]] > 1:
            window_char_map[s[l]] -= 1
            l += 1

    def update_best_window() -> None:
        nonlocal best_window
        window = s[l:r]
        if best_window is None or len(best_window) > len(window):
            best_window = window

    def contract_left() -> str:
        nonlocal l

        c = s[l]
        window_char_map[c] -= 1
        l += 1

        return c

    grow_while_missing_chars()
    contract_while_excess()
    update_best_window()

    while True:
        lost = contract_left()
        ok = grow_until(lost)

        if not ok:
            break

        contract_while_excess()
        update_best_window()

    return best_window


print(len(min_window('jiujitsu')))  # 5
print(len(min_window('aaabaaacaaaabca')))  # 3
