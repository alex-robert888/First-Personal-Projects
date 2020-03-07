

from Errors.Errors import *

import datetime


class Validator(object):

    def __init__(self):
        pass

    @staticmethod
    def validate_positive_integer(val):
        print(val)

        try:
            assert int(val) >= 0
        except:
            raise InvalidParameters()
        else:
            return int(val)

    @staticmethod
    def validate_rental(rentals, client_id, movie_id):
        for r in rentals:
            # if the movie is unavailable
            if r.movie_id == movie_id and r.return_date == datetime.date(2000, 1, 1):
                raise InvalidRentalError("Unavailable movie.")
            # if client has an unreturned movie which due date passed
            if r.client_id == client_id and r.return_date == datetime.date(2000, 1, 1) and datetime.date.today() > r.due_date:
                raise InvalidRentalError("Client has an unreturned movie that passed the due date!")
    @staticmethod
    def validate_return(rentals, client_id, movie_id):
        for r in rentals:
            # if there exists this rental and it was not returned
            if r.movie_id == movie_id and r.client_id == client_id and r.return_date == datetime.date(2000, 1, 1):
                return r
        raise InvalidReturnError("It is either that the rental does not exist or that it was already returned")

