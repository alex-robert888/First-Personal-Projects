
from Repository.Repository import Repository


class FileBasedRepository(Repository):
    def __init__(self, file):
        super().__init__()
        self.file = file

    def read_data_from_file(self):
        """
        Reads the entities from file and stores them into the list
        Note: This happens only at the startup of the application

        """

        fin = open(self.file, "r")
        entities = fin.readlines()
        fin.close()
        return entities


    def write_data_to_file(self):
        """
        Reads the entities from file and stores thme into the list
        Note: That only happens just before exiting the application

        """
        fout = open(self.file, "w")

        for entity in self.repo:
            fout.write(str(entity) + '\n')

        fout.close()




