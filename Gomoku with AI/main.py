
from ui import UI
from factory import Factory
from Move import Move

class MainGame(object):
    def __init__(self):
        self._factory = Factory()
        self._ui = None
        self._board = self._factory.create_new("Board", *[19])
        self._human = self._factory.create_new("Human", *[self._board])
        self._computer = self._factory.create_new("Computer", *[self._board])

    def run(self):
        """
        Runs the game: initializes the user chosen UI and starts the corresponding game loop
        """
        # 1. Choose UI type and create it
        self._ui = self._factory.create_new(UI.choose_ui(), *[self._human, self._computer, self._board])

        # 2. Start game loop
        self._ui.start_game_loop()


if __name__ == "__main__":
    main_game = MainGame()
    main_game.run()