

from Errors.Errors import *
from abc import ABC


class Repository(object):

    def __init__(self):
        self.__repo = list()

    # region Getter

    @property
    def repo(self):
        return self.__repo

    @repo.setter
    def repo(self, val):
        self.__repo = val

    # endregion

    # region Functionalities

    def add_new_entity(self, entity):
        # check if duplicate
        # print(entity)
        for ent in self.__repo:
            if ent == entity:
                raise DuplicateError()

        # if unique, add it
        self.repo.append(entity)

    def remove_entity(self, entity_id):
        # remove entity if it exists
        for entity in self.__repo:
            if entity.id == entity_id:
                self.__repo.remove(entity)
                return

        # if entity does not exist, raise EntityNotFound error
        raise EntityNotFound()

    def search_entity(self, entity_id):
        for entity in self.__repo:
            if str(entity.id) == str(entity_id):
                return entity

        return None

    def replace_entity(self, old_entity, new_entity):
        index = self.__repo.index(old_entity)
        self.__repo.remove(old_entity)
        self.__repo.insert(index, new_entity)

    def update_entity(self, old_id, updated_entity):

        if updated_entity in self.__repo and old_id != updated_entity.id:
            raise DuplicateError()

        for entity in self.__repo:
            if entity.id == old_id:
                self.replace_entity(entity, updated_entity)
                return
        raise EntityNotFound()

    def get_repo_copy(self):
        return self.__repo[:]
    # endregion

