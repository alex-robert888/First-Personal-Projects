

class Client(object):

    def __init__(self, id, name):
        self.__id = id
        self.__name = name

    # region Getters

    @property
    def id(self):
        return self.__id

    @property
    def name(self):
        return self.__name

    # endregion

    # region Setters

    @id.setter
    def id(self, value):
        self.__id = value

    @name.setter
    def name(self, value):
        self.__name = value
    # endregion

    # region Overloads
    def __str__(self):
        return str(self.id) + ";" + str(self.name)

    def __eq__(self, other):
        return self.__id == other.__id
    # endregion
