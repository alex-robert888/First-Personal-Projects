


from Entities.Rental import Rental
from Services.Validator import Validator
from Errors.Errors import *
from Services.ServicesClients import ServicesClients
from Services.ServicesMovies import ServicesMovies

import datetime


class ServicesRentals(object):

    def __init__(self, repo_rentals):
        self.__repo_rentals = repo_rentals

    @property
    def repo_rentals(self):
        return self.__repo_rentals

    @property
    def repo_rentals_list(self):
        return self.__repo_rentals.repo

    def remove_rental(self, id):
        self.repo_rentals.remove_entity(id)

    def add_rental(self, id, client_id, movie_id, rented_date, due_date, return_date):
        self.repo_rentals.add_new_entity(Rental(int(id), int(client_id), int(movie_id), rented_date, due_date, return_date))

    def rent_movie(self, id, client_id, movie_id, due_year, due_month, due_day):

        # parameters validations
        id = Validator.validate_positive_integer(id)
        client_id = Validator.validate_positive_integer(client_id)
        movie_id = Validator.validate_positive_integer(movie_id)
        #print(client_id)
        try:
            due_date = datetime.date(int(due_year), int(due_month), int(due_day))
        except:
            raise InvalidDateError()

        # after these validations, there are still other validations to be performed
        # the following conditions are to be checked:

        # 1. A movie that was rented cannot be rented again, unless it was returned
        # 2. A client can rent a movie only if they have no rented movies that passed their due date for return
        repo_copy = self.__repo_rentals.get_repo_copy()
        Validator.validate_rental(repo_copy, client_id, movie_id)

        # All conditions being fulfilled, rental can be performed
        self.__repo_rentals.add_new_entity(Rental(id, client_id, movie_id, datetime.date.today(), due_date, datetime.date(2000, 1, 1)))

    def return_movie(self, client_id, movie_id):
        client_id = Validator.validate_positive_integer(client_id)
        movie_id = Validator.validate_positive_integer(movie_id)

        # Validations to be performed:
        # 1. There exist a rental with this client and movie
        # 2. The movie has not been returned yet
        repo_copy = self.__repo_rentals.get_repo_copy()
        rental = Validator.validate_return(repo_copy, client_id, movie_id)

        self.repo_rentals.update_entity(rental.id, Rental(rental.id, client_id, movie_id, rental.rented_date, rental.due_date, datetime.date.today()))
        return rental
    def freq_most_rented_movies(self):
        freq = 1000 * [0]

        for entity in self.__repo_rentals.repo:
            if entity.return_date == datetime.date(2000, 1, 1):
                freq[entity.movie_id] += abs((datetime.date.today() - entity.rented_date).days)
            else:
                freq[entity.movie_id] += abs((entity.return_date - entity.rented_date).days)

        return freq

    def freq_most_active_clients(self):
        freq = 1000 * [0]

        for entity in self.repo_rentals.repo:
            if entity.return_date == datetime.date(2000, 1, 1):
                freq[entity.client_id] += abs((datetime.date.today() - entity.rented_date).days)
            else:
                freq[entity.client_id] += abs((entity.return_date - entity.rented_date).days)
        return freq

    def freq_late_rentals(self):
        freq = 1000 * [0]

        for entity in self.repo_rentals.repo:
            if entity.return_date == datetime.date(2000, 1, 1) and entity.due_date < datetime.date.today():
                freq[entity.movie_id] += abs((datetime.date.today() - entity.due_date).days)

        return freq
