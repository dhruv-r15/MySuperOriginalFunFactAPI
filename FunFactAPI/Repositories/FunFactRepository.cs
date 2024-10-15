using System.Collections.Generic;
using System.Linq;
using FunFactAPI.Models;

namespace FunFactAPI.Repositories
{
    public class FunFactRepository
    {
        private List<FunFact> _funFacts = new List<FunFact>();
        private int _nextId = 1;

        public IEnumerable<FunFact> GetAll() => _funFacts;

        public FunFact GetById(int id) => _funFacts.FirstOrDefault(f => f.Id == id);

        public IEnumerable<FunFact> GetByCategory(string category) =>
            _funFacts.Where(f => f.Category.ToLower() == category.ToLower());

        public IEnumerable<FunFact> GetByCharacter(string character) =>
            _funFacts.Where(f => f.Character.ToLower() == character.ToLower());

        public FunFact Add(FunFact funFact)
        {
            funFact.Id = _nextId++;
            _funFacts.Add(funFact);
            return funFact;
        }

        public FunFact Update(FunFact funFact)
        {
            var existing = GetById(funFact.Id);
            if (existing != null)
            {
                existing.Category = funFact.Category;
                existing.Fact = funFact.Fact;
                existing.Source = funFact.Source;
                existing.Character = funFact.Character;
            }
            return existing;
        }

        public bool Delete(int id)
        {
            var funFact = GetById(id);
            if (funFact != null)
            {
                _funFacts.Remove(funFact);
                return true;
            }
            return false;
        }

        public FunFact Vote(int id, bool upvote)
        {
            var funFact = GetById(id);
            if (funFact != null)
            {
                funFact.Votes += upvote ? 1 : -1;
            }
            return funFact;
        }
    }
}
