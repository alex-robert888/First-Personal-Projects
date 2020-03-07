
from Move import Move

class Board(object):
    def __init__(self, width):
        self.width = width
        self.__board = [[0 for i in range(self.width + 1)] for j in range(self.width + 1)]

    @property
    def board(self):
        return self.__board

    def set_cell(self, x, y, val):
        self.__board[y][x] = val

    def get_cell(self, x, y):
        return self.__board[y][x]

    def print(self):
        for i in range(1, self.width + 1):
            for j in range(1, self.width + 1):
                print(self.__board[i][j], end=" ")
            print('')

    def is_winner_move(self, x, y):
        """
        Determines whether the last performed move is a winner one
        :param x: The x coordinate of the move
        :param y: The y coordinate of the move
        :return: true / false - winner move / non-winner move
        """

        # direction vectors(like those used in BF algorithm)
        dir_col = [0, 1, 1, 1, 0, -1, -1, -1]
        dir_row = [-1, -1, 0, 1, 1, 1, 0, -1]

        for d in range(8):
            temp_move = Move(x, y)
            if temp_move.x + 4 * dir_col[d] < 1 or temp_move.x + 4 * dir_col[d] > self.width or temp_move.y + 4 * dir_row[d] < 1 or temp_move.y + 4 * dir_row[d] > self.width:
                continue

            consecutive = 1
            for i in range(4):
                temp_move.x += dir_col[d]
                temp_move.y += dir_row[d]

                if self.__board[temp_move.y][temp_move.x] == self.__board[y][x]:
                    consecutive += 1
                else:
                    break

            if consecutive == 5:
                return True

        return False
