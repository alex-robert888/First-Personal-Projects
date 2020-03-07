
from gui import GUI
from console_ui import ConsoleUI
from Board import Board
from Human import Human
from Computer import Computer

class Factory(object):
    """
    Specific DDD Class, which is responsible for creating new objects

    === Attributes ===
    types : dict() - a dictionary containing all type names as key and their corresponding types as values(other classes
    are able to create new objects of a given type by providing a string containing the type name)

    """

    def __init__(self):
        self.__types = dict(
            {
                "GUI": GUI,
                "ConsoleUI": ConsoleUI,
                "Board": Board,
                "Human": Human,
                "Computer": Computer
            }
        )

    def create_new(self, type_name, *params):
        """
        Create a new object of a given type
        :param type_name: String containing the name of the type
        :return: An object of the requested type
        """
        return self.__types[type_name](*params)

