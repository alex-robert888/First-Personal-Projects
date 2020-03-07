

class DuplicateError(Exception):
    def __init__(self):
        Exception.__init__(self, "Duplicates are not allowed.")


class InvalidParameters(Exception):
    def __init__(self):
        Exception.__init__(self, "Invalid parameters.")


class EntityNotFound(Exception):
    def __init__(self):
        Exception.__init__(self, "There is no entity with the specified id.")


class InvalidDateError(Exception):
    def __init__(self):
        Exception.__init__(self, "Invalid date.")


class InvalidRentalError(Exception):
    pass


class InvalidReturnError(Exception):
    pass


class EmptyStackError(Exception):
    def __init__(self, stack_name):
        Exception.__init__(self, "The stack named " + stack_name + " is empty")


class InvalidRepositoryTypeError(Exception):
    def __init__(self, stack_name):
        Exception.__init__(self, "Invalid Repository Type!")

