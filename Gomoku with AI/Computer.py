
from Player import Player
from Move import Move

class Computer(Player):
    def __init__(self, board):
        super().__init__(board)
        self.copy_board_mat = [[0 for i in range(self.board.width + 1)] for j in range(self.board.width + 1)]
        self.minus_infinity = -100000000
        self.plus_infinity = 100000000
        self.limit_depth = 3

    def place_piece(self, x, y):
        self.board.set_cell(x, y, 2)
        self.board.print()

    def get_chain_score(self, consecutive_pieces, open_ends, computer_chain, own_turn):
        sign = -1 if not computer_chain else 1

        if consecutive_pieces == 5 and own_turn:
            return sign * self.plus_infinity

        if open_ends == 0 and consecutive_pieces < 5:
            return 0

        if consecutive_pieces == 4:
            if open_ends == 1:
                if own_turn:
                    return sign * self.plus_infinity
                return sign * 500

            if open_ends == 2:
                if own_turn:
                    return sign * self.plus_infinity
                return sign * self.plus_infinity / 2

        if consecutive_pieces == 3:
            if open_ends == 1:
                if own_turn:
                    return sign * 70
                return sign * 50
            if open_ends == 2:
                if own_turn:
                    return sign * 500000
                return sign * 500

        if consecutive_pieces == 2:
            if open_ends == 1:
                if own_turn:
                    return sign * 7
                return sign * 4
            if open_ends == 2:
                if computer_chain:
                    return sign * 15
                return sign * 9

        if consecutive_pieces == 1:
            if open_ends == 1:
                if computer_chain:
                    return sign * 2
                return sign * 1
            if open_ends == 2:
                if computer_chain:
                    return sign * 3
                return sign * 2

        return 0

    def get_chain_score_simpler(self, consecutive_pieces, open_ends, computer_chain):
        sign = -1 if not computer_chain else 1

        if consecutive_pieces == 5:
            return sign * self.plus_infinity

        if open_ends == 0 and consecutive_pieces < 5:
            return 0

        if consecutive_pieces == 4:
            if open_ends == 1:
                return sign * self.plus_infinity

            if open_ends == 2:
                 return sign * self.plus_infinity

        if consecutive_pieces == 3:
            if open_ends == 1:
                return sign * 70
            if open_ends == 2:
                return sign * 500000

        if consecutive_pieces == 2:
            if open_ends == 1:
                return sign * 7
            if open_ends == 2:
                return sign * 15

        if consecutive_pieces == 1:
            if open_ends == 1:
                return sign * 2
            if open_ends == 2:
                return sign * 3

        return 0

    def get_horizontal_chains_score(self, whose_turn):
        score = 0

        for y in range(1, self.board.width + 1):
            consecutive = 0
            open_ends = 0

            if self.copy_board_mat[y][1] == 0:
                open_ends = 1
            else:
                consecutive = 1

            for x in range(2, self.board.width + 1):
                if self.copy_board_mat[y][x] != 0:
                    if self.copy_board_mat[y][x] == self.copy_board_mat[y][x - 1]:
                        consecutive += 1
                    elif self.copy_board_mat[y][x - 1] == 0:
                        consecutive = 1
                        open_ends = 1
                    else:
                        consecutive = 1
                        open_ends = 0

                else:
                    if consecutive > 0:
                        open_ends += 1
                        computer_chain = True if self.copy_board_mat[y][x - 1] == 2 else False
                        own_turn = 1 if whose_turn == self.copy_board_mat[y][x - 1] else 0
                        score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)
                        consecutive = 0
                        open_ends = 1

            if self.copy_board_mat[y][self.board.width] == 0 and consecutive > 0:
                computer_chain = True if self.copy_board_mat[y][self.board.width - 1] == 2 else False
                own_turn = 1 if whose_turn == self.copy_board_mat[y][self.board.width - 1] else 0
                score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)

        return score

    def get_vertical_chains_score(self, whose_turn):
        score = 0

        for x in range(1, self.board.width + 1):
            consecutive = 0
            open_ends = 0

            if self.copy_board_mat[1][x] == 0:
                open_ends = 1
            else:
                consecutive = 1

            for y in range(2, self.board.width + 1):
                if self.copy_board_mat[y][x] != 0:
                    if self.copy_board_mat[y][x] == self.copy_board_mat[y - 1][x]:
                        consecutive += 1
                    elif self.copy_board_mat[y - 1][x] == 0:
                        consecutive = 1
                        open_ends = 1
                    else:
                        consecutive = 1
                        open_ends = 0

                else:
                    if consecutive > 0:
                        open_ends += 1
                        computer_chain = True if self.copy_board_mat[y - 1][x] == 2 else False
                        own_turn = 1 if whose_turn == self.copy_board_mat[y - 1][x] else 0
                        score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)
                        consecutive = 0
                        open_ends = 1

            if self.copy_board_mat[self.board.width][x] == 0 and consecutive > 0:
                computer_chain = True if self.copy_board_mat[self.board.width - 1][x] == 2 else False
                own_turn = 1 if whose_turn == self.copy_board_mat[self.board.width - 1][x] else 0
                score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)

        return score

    def get_parallel_with_main_diagonal_chains_score(self, whose_turn):

        score = 0

        for x in range(1, self.board.width + 1):
            consecutive = 0
            open_ends = 0

            if self.copy_board_mat[1][x] == 0:
                open_ends = 1
            else:
                consecutive = 1

            for y in range(2, self.board.width + 1):
                if self.copy_board_mat[y][x] != 0:
                    if self.copy_board_mat[y][x] == self.copy_board_mat[y - 1][x - 1]:
                        consecutive += 1
                    elif self.copy_board_mat[y - 1][x - 1] == 0:
                        consecutive = 1
                        open_ends = 1
                    else:
                        consecutive = 1
                        open_ends = 0

                else:
                    if consecutive > 0:
                        open_ends += 1
                        computer_chain = True if self.copy_board_mat[y - 1][x - 1] == 2 else False
                        own_turn = 1 if whose_turn == self.copy_board_mat[y - 1][x - 1] else 0
                        score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)
                        consecutive = 0
                        open_ends = 1
            score = 0
            if self.copy_board_mat[self.board.width][x] == 0 and consecutive > 0:
                computer_chain = True if self.copy_board_mat[self.board.width - 1][x] == 2 else False
                own_turn = 1 if whose_turn == self.copy_board_mat[self.board.width - 1][x] else 0
                score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)
                score = 0

        return score


    def get_parallel_with_secondary_diagonal_chains_score(self, whose_turn):
        score = 0

        for x in range(1, self.board.width + 1):
            consecutive = 0
            open_ends = 0

            if self.copy_board_mat[1][x] == 0:
                open_ends = 1
            else:
                consecutive = 1

            for y in range(2, self.board.width + 1):
                if self.copy_board_mat[y][x] != 0:
                    if self.copy_board_mat[y][x] == self.copy_board_mat[y - 1][x - 1]:
                        consecutive += 1
                    elif self.copy_board_mat[y - 1][x - 1] == 0:
                        consecutive = 1
                        open_ends = 1
                    else:
                        consecutive = 1
                        open_ends = 0

                else:
                    if consecutive > 0:
                        open_ends += 1
                        computer_chain = True if self.copy_board_mat[y - 1][x - 1] == 2 else False
                        own_turn = 1 if whose_turn == self.copy_board_mat[y - 1][x - 1] else 0
                        score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)
                        consecutive = 0
                        open_ends = 1

            score = 0
            if self.copy_board_mat[self.board.width][x] == 0 and consecutive > 0:
                computer_chain = True if self.copy_board_mat[self.board.width - 1][x - 1] == 2 else False
                own_turn = 1 if whose_turn == self.copy_board_mat[self.board.width - 1][x - 1] else 0
                score += self.get_chain_score(consecutive, open_ends, computer_chain, own_turn)
                score = 0

        return score

    def evaluate_board(self, computer_turn):
        whose_turn = 2 if computer_turn else 1

        score = self.get_vertical_chains_score(whose_turn) + self.get_horizontal_chains_score(whose_turn) \
                + self.get_parallel_with_main_diagonal_chains_score(whose_turn) \
                + self.get_parallel_with_secondary_diagonal_chains_score(whose_turn)
        # score = self.get_vertical_chains_score(whose_turn)
        return score

    def minimax(self, maximizer_turn, depth):
        score = self.evaluate_board(maximizer_turn)

        #### is this condition ok? ####
        if score == self.plus_infinity or score == self.minus_infinity or depth == self.limit_depth:
            return score

        best_val = None

        if maximizer_turn:
            best_val = self.minus_infinity

            for y in range(1, self.board.width + 1):
                for x in range(1, self.board.width + 1):
                    if self.copy_board_mat[y][x] != 0:
                        continue

                    self.copy_board_mat[y][x] = 2

                    tried_move_val = self.minimax(False, depth + 1)
                    if tried_move_val > best_val:
                        best_val = tried_move_val

                    self.copy_board_mat[y][x] = 0

            return best_val
        else:
            best_val = self.plus_infinity

            for y in range(1, self.board.width + 1):
                for x in range(1, self.board.width + 1):
                    if self.copy_board_mat[y][x] != 0:
                        continue

                    self.copy_board_mat[y][x] = 1

                    tried_move_val = self.minimax(True, depth + 1)
                    if tried_move_val < best_val:
                        best_val = tried_move_val

                    self.copy_board_mat[y][x] = 0

            return best_val

    def minimax_simpler(self, maximizer_turn, depth, x, y):
        self.board.set_cell(x, y, 2)
        if self.board.is_winner_move(x, y):
            return self.plus_infinity
        self.board.set_cell(x, y, 0)

        score = self.evaluate_board(maximizer_turn)
        return score

    def refresh_copy_board(self):
        for y in range(1, self.board.width + 1):
            for x in range(1, self.board.width + 1):
                self.copy_board_mat[y][x] = self.board.get_cell(x, y)

    def find_optimal_move(self):
        """
        Applying minimax algorithm, determines the best move for a given board configuration
        :return: (x, y) - the coordinates of the best move
        """

        best_move = Move(0, 0)
        best_move.val = self.minus_infinity

        self.refresh_copy_board()

        for y in range(1, self.board.width + 1):
            for x in range(1, self.board.width + 1):
                if self.board.get_cell(x, y) != 0:
                    continue

                self.copy_board_mat[y][x] = 2

                tried_move_val = self.minimax_simpler(False, 1, x, y)
                if tried_move_val == self.plus_infinity:
                    best_move.x = x
                    best_move.y = y
                    best_move.val = tried_move_val
                    return best_move

                if tried_move_val > best_move.val:
                    best_move.x = x
                    best_move.y = y
                    best_move.val = tried_move_val

                self.copy_board_mat[y][x] = 0

        return best_move



