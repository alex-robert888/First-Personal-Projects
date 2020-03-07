

class Reader(object):
    def __init__(self):
        pass

    @staticmethod
    def read_integer_in_range(lower_bound, upper_bound, msg):
        """
        Reads a valid integer with its value in a given range
        :param lower_bound: The lower bound of the range
        :param upper_bound: The upper bound of the range
        :return: Valid input integer
        """
        while True:
            try:
                option = int(input(msg))
                assert option >= lower_bound
                assert option <= upper_bound
            except ValueError or AssertionError:
                print("Please choose a number between " + str(lower_bound) + " and " + str(upper_bound))
            else:
                return option

    @staticmethod
    def read_cell_coordinates(board_width):
        """
        Reads a valid cell from a square board of a given width
        :param board_width: The width of the board
        :return: The valid input cell
        """
        x = Reader.read_integer_in_range(1, board_width, "x: ")
        y = Reader.read_integer_in_range(1, board_width, "y: ")

        return x, y
