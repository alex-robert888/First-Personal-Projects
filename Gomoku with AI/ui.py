
from game_state import GameState
from reader import Reader

class UI(object):
    def __init__(self, human, computer, board):
        self.print_successfully_created_message()
        self.game_state = GameState.RUNNING
        self.__human = human
        self.__computer = computer
        self.__board = board

    def print_successfully_created_message(self):
        pass

    def start_game_loop(self):
        pass

    @staticmethod
    def choose_ui():
        print("Choose which user-interface to go on with")
        print("1. Console-based UI")
        print("2. GUI")

        # read valid user option
        option = Reader.read_integer_in_range(1, 2, "Option number: ")
        return "ConsoleUI" if option == 1 else "GUI"
