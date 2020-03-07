
class Operation(object):

    def __init__(self, name, function, *parameters):
        self.__name = name
        self.__function = function
        self.__parameters = parameters

    @property
    def name(self):
        return  self.__name

    @property
    def parameters(self):
        return self.__parameters

    @property
    def function(self):
        return self.__function