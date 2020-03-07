

from Repository.Repository import *
from Entities.Client import Client
from Services.Validator import Validator

import re


class ServicesClients(object):

    def __init__(self, repo_clients):
        self.__repo_clients = Repository()
        self.__repo_clients = repo_clients

    @property
    def repo_clients_list(self):
        return self.__repo_clients.repo

    @property
    def repo_clients(self):
        return self.__repo_clients

    def add_client(self, id, name):
        # validations
        # if the id is a valid positive integer, then it is converted from str into int
        id = Validator.validate_positive_integer(id)

        # add client
        self.__repo_clients.add_new_entity(Client(id, name))

    def remove_client(self, id):
        # validations
        id = Validator.validate_positive_integer(id)

        # remove client
        self.__repo_clients.remove_entity(id)

    def update_client(self, id, new_id, name):
        id = Validator.validate_positive_integer(id)
        new_id = Validator.validate_positive_integer(new_id)

        self.__repo_clients.update_entity(id, Client(new_id, name))

    def search_by_field(self, option, input):
        matching_items = list()

        for entity in self.__repo_clients.repo:
            if option == 1:
                if re.search(input, str(entity.id), re.IGNORECASE):
                    matching_items.append(entity)
            elif option == 2:
                if re.search(input, entity.name, re.IGNORECASE):
                    matching_items.append(entity)

        return matching_items

