
from ui import UI
from game_state import GameState
from reader import Reader
from Move import Move
import random

class ConsoleUI(UI):
    def __init__(self, human, computer, board):
        super().__init__(human, computer, board)
        self.__human = human
        self.__computer = computer
        self.__board = board

    def print_successfully_created_message(self):
        print("Console user-interface was successfully created.\n")

    def start_game_loop(self):
        """
        Starts the classical game loop.
        In this case, two different scenarios are expected within the game loop:
         1) It is the human's turn. Then, an input is awaited, indicating the position of the cell where the players
         wills to place their next piece
         2) It is the computer's turn. Then, the computer is expected to compute via minimax algorithm its next move.
        """
        whose_turn = 1
        while self.game_state == GameState.RUNNING:
            if whose_turn == 1:     # human player's turn
                x, y = Reader.read_cell_coordinates(19)
                self.__human.place_piece(x, y)
                if self.__board.is_winner_move(x, y):
                    print("Player won!")
                    game_state = GameState.SHUTDOWN
            else:                   # computer's turn
                input("press key to let computer analyse the board and perform its next move..")
                best_move = self.__computer.find_optimal_move()
                self.__computer.place_piece(best_move.x, best_move.y)
                if self.__board.is_winner_move(x, y):
                    print("Computer won!")
                    game_state = GameState.SHUTDOWN

            whose_turn = 3 - whose_turn     # switch whose turn is to the other player

