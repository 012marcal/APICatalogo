﻿using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria> ,ICategoriaRepository
    {

        public CategoriaRepository(DatabaseContext context) : base(context)
        {
            
        }
        
        

    }
}
