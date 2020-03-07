
from Repository.Repository import Repository
import pickle


class  BinaryFileBasedRepository(Repository):
    def __init__(self, file):
        super().__init__()
        self.file = file

    def write_to_binary_file(self):
        fout= open(self.file, "wb")
        pickle.dump(self.repo, fout)
        fout.close()

    def read_from_binary_file(self):
        try:
            fin = open(self.file, "rb")
            return pickle.load(fin)
        except EOFError:
            return []
