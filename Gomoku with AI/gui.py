from ui import UI
from game_state import GameState

import pygame
import time

class GUI(UI):
    def __init__(self, human, computer, board):
        super().__init__(human, computer, board)
        self.__human = human
        self.__computer = computer
        self.__board = board
        pygame.init()

        self.light_square_color = (156, 158, 170)
        self.dark_square_color = (75, 27, 111)
        self.black = (0, 0, 0)
        self.white = (255, 255, 255)

        self.screen_width = 760
        self.screen_height = 760
        self.cell_width = 40

        self.screen = pygame.display.set_mode((self.screen_width, self.screen_height))
        pygame.display.set_caption("Gomoku")
        self.screen.fill((0, 0, 0))
        pygame.display.flip()

    def print_successfully_created_message(self):
        print("Graphical user-interface was successfully created.\n")

    def draw_board(self):
        for y in range(19):
            for x in range(19):
                color = self.light_square_color if (x + y) % 2 == 0 else self.dark_square_color
                pygame.draw.rect(self.screen, color, [x * self.cell_width, y * self.cell_width, self.cell_width,
                                                      self.cell_width])

                piece = self.__board.get_cell(x + 1, y + 1)
                if piece != 0:
                    piece_color = self.white if piece == 1 else self.black
                    pygame.draw.circle(self.screen, piece_color,
                                        [x * self.cell_width + self.cell_width // 2, y * self.cell_width + self.cell_width // 2],
                                        12, 0
                                        )

    def draw_everything(self):
        self.draw_board()

        pygame.display.update()

    def start_game_loop(self):
        # computer_move = self.__computer.find_optimal_move()

        while self.game_state == GameState.RUNNING:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    self.game_state = GameState.SHUTDOWN
                elif event.type == pygame.MOUSEBUTTONUP:
                    x, y = pygame.mouse.get_pos()
                    x = x // 40 + 1
                    y = y // 40 + 1

                    if self.__board.get_cell(x, y) == 0:
                        self.__human.place_piece(x, y)
                    else:
                        print("Invalid move!")
                        break

                    if self.__board.is_winner_move(x, y):
                        text = "YOU WON!"
                        font = pygame.font.SysFont('freesansbold.ttf',210)
                        textsurface = font.render(text, True, (0, 176, 240))
                        self.draw_everything()
                        self.screen.blit(textsurface, (0, 380))
                        pygame.display.update()
                        time.sleep(2)
                        self.game_state = GameState.SHUTDOWN
                    else:
                        computer_move = self.__computer.find_optimal_move()
                        self.__computer.place_piece(computer_move.x, computer_move.y)
                        if self.__board.is_winner_move(computer_move.x, computer_move.y):
                            text = "YOU LOST!"
                            font = pygame.font.SysFont('freesansbold.ttf', 200)
                            textsurface = font.render(text, True, (0, 176, 240))
                            self.draw_everything()
                            self.screen.blit(textsurface, (0, 380))
                            pygame.display.update()
                            time.sleep(2)
                            self.game_state = GameState.SHUTDOWN

                self.draw_everything()
