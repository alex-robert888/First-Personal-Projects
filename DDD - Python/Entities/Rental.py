

class Rental(object):

    def __init__(self, id, client_id, movie_id, rented_date, due_date, return_date):
        self.__id = id
        self.__client_id = client_id
        self.__movie_id = movie_id
        self.__rented_date = rented_date
        self.__due_date = due_date
        self.__return_date = return_date

    # region Getters

    @property
    def id(self):
        return self.__id

    @property
    def client_id(self):
        return self.__client_id

    @property
    def movie_id(self):
        return self.__movie_id

    @property
    def rented_date(self):
        return self.__rented_date

    @property
    def due_date(self):
        return self.__due_date

    @property
    def return_date(self):
        return self.__return_date

    # endregion

    # region Setters

    @id.setter
    def id(self, value):
        self.__id = value

    @client_id.setter
    def client_id(self, value):
        self.__client_id = value

    @movie_id.setter
    def movie_id(self, value):
        self.__movie_id = value

    @return_date.setter
    def return_date(self, value):
        self.__return_date = value

    @due_date.setter
    def due_date(self, value):
        self.__due_date = value

    @rented_date.setter
    def rented_date(self, value):
        self.rented_date = value

    # endregion

    # region Overloads

    def __str__(self):
        return str(self.id) + ";" + str(self.client_id) + ";" + str(self.movie_id) + \
               ";" + str(self.rented_date) + ";" + str(self.due_date) + ";" \
               + str(self.return_date)

    def __eq__(self, other):
        return self.__id == other.__id

    # endregion