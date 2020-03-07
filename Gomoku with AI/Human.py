
from Player import Player

class Human(Player):
    def __init__(self, board):
        super().__init__(board)

    def place_piece(self, x, y):
        self.board.set_cell(x, y, 1)
        self.board.print()