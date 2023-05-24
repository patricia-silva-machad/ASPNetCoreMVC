﻿using Lanches.Context;
using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Repositories {
    public class LancheRepository : ILancheRepository 
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext contexto) {
            _context = contexto;
        }
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categories);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
                                    .Where(l => l.IsLanchePreferido)
                                    .Include(c => c.Categories);
        public Lanche GetLancheById(int lancheId) {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
