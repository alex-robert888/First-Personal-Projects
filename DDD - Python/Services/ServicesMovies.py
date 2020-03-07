
from Repository.Repository import Repository
from Entities.Movie import Movie
from Services.Validator import Validator
from Errors.Errors import *

import re

class ServicesMovies(object):

    def __init__(self, repo_movies):
        self.__repo_movies = repo_movies

    @property
    def repo_movies(self):
        return self.__repo_movies

    @property
    def repo_movies_list(self):
        return self.__repo_movies.repo

    def add_movie(self, id, title, description, genre):
        id = Validator.validate_positive_integer(id)

        self.__repo_movies.add_new_entity(Movie(id, title, description, genre))

    def remove_movie(self, id):
        id = Validator.validate_positive_integer(id)

        self.__repo_movies.remove_entity(id)

    def update_movie(self, id, new_id, title, description, genre):
        id = Validator.validate_positive_integer(id)
        new_id = Validator.validate_positive_integer(new_id)

        self.__repo_movies.update_entity(id, Movie(new_id, title, description, genre))

    def search_by_field(self, option, input):
        matching_items = list()

        for entity in self.__repo_movies.repo:
            if option == 1:
                if re.search(input, str(entity.id), re.IGNORECASE):
                    matching_items.append(entity)
            elif option == 2:
                if re.search(input, entity.title, re.IGNORECASE):
                    matching_items.append(entity)
            elif option == 3:
                if re.search(input, entity.description, re.IGNORECASE):
                    matching_items.append(entity)
            elif option == 4:
                if re.search(input, entity.genre, re.IGNORECASE):
                    matching_items.append(entity)

        return matching_items
