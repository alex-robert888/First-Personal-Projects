

class Movie(object):
    '''

    Class representing the movie entity
    :param: id - the unique id of the movie
    :param: title - the title of the movie
    :param: description - a brief description of the movie
    :param: genre - the genre of the movie

    '''

    def __init__(self, id, title, description, genre):
        self.__id = id
        self.__title = title
        self.__description = description
        self.__genre = genre

    # region Getters

    @property
    def id(self):
        return self.__id

    @property
    def title(self):
        return self.__title

    @property
    def description(self):
        return self.__description

    @property
    def genre(self):
        return self.__genre

    # endregion

    #region Setters

    @id.setter
    def id(self, value):
        self.__id = value

    @title.setter
    def title(self, value):
        self.__title = value

    @description.setter
    def description(self, value):
        self.__description = value

    @genre.setter
    def genre(self, value):
        self.__genre = value

    #endregion

    # region Overloads
    def __str__(self):
        return str(self.id) + ";" + str(self.title) + ";" + str(self.description) + \
               ";" + str(self.genre)

    def __eq__(self, other):
        return self.__id == other.__id
    # endregion
