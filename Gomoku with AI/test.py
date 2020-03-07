
import unittest
from Board import Board
from main import MainGame

class TestBoard(unittest.TestCase):

    def setUp(self):
        unittest.TestCase.setUp(self)
        self.test_board = Board(19)

    def tearDown(self):
        unittest.TestCase.tearDown(self)

    def test_set_cell_and_get_cell(self):
        self.test_board.set_cell(12, 9, 1)
        assert self.test_board.get_cell(12, 9) == 1

        self.test_board.set_cell(12, 9, 2)
        assert self.test_board.get_cell(12, 9) == 2

        self.test_board.set_cell(12, 9, 0)
        assert self.test_board.get_cell(12, 9) == 0

    def test_is_winner_move(self):
        self.test_board.set_cell(1, 1, 1)
        self.test_board.set_cell(2, 2, 1)
        self.test_board.set_cell(3, 3, 1)
        self.test_board.set_cell(4, 4, 1)

        self.test_board.set_cell(5, 5, 1)
        assert self.test_board.is_winner_move(5, 5) == True
        self.test_board.set_cell(5, 5, 0)

        self.test_board.set_cell(5, 6, 1)
        assert self.test_board.is_winner_move(5, 6) == False
        self.test_board.set_cell(5, 6, 0)

class TestComputer(unittest.TestCase):

    def setUp(self):
        unittest.TestCase.setUp(self)
        self.test_main_game = MainGame()

    def tearDown(self):
        unittest.TestCase.tearDown(self)

    def test_find_optimal_move(self):
        self.test_main_game._board.set_cell(3, 2, 1)
        self.test_main_game._board.set_cell(3, 3, 2)

        self.test_main_game._board.set_cell(3, 1, 1)
        self.test_main_game._board.set_cell(3, 4, 2)

        self.test_main_game._board.set_cell(4, 1, 1)
        self.test_main_game._board.set_cell(3, 5, 2)

        self.test_main_game._board.set_cell(5, 1, 1)
        self.test_main_game._board.set_cell(3, 6, 2)

        optimal_move = self.test_main_game._computer.find_optimal_move()
        assert optimal_move.x == 3
        assert optimal_move.y == 7

    def test_minimax(self):
        self.test_main_game._board.set_cell(3, 2, 1)
        self.test_main_game._board.set_cell(3, 3, 2)

        self.test_main_game._board.set_cell(3, 1, 1)
        self.test_main_game._board.set_cell(3, 4, 2)

        self.test_main_game._board.set_cell(4, 1, 1)
        self.test_main_game._board.set_cell(3, 5, 2)

        self.test_main_game._board.set_cell(5, 1, 1)
        self.test_main_game._board.set_cell(3, 6, 2)

        optimal_score = self.test_main_game._computer.minimax_simpler(False, 1, 3, 7)
        assert optimal_score == self.test_main_game._computer.plus_infinity

