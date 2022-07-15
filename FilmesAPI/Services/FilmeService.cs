using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs.Filme;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadFilmeDto AdicionarFilme(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> RecuperarFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
            } else
            {
                filmes = _context.Filmes.Where(filme => filme
                        .ClassificacaoEtaria <= classificacaoEtaria)
                    .ToList();
            }
            return _mapper.Map<List<ReadFilmeDto>>(filmes);
        }

        public ReadFilmeDto RecuperarFilmePorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                return _mapper.Map<ReadFilmeDto>(filme);
            }

            return null;
        }

        public Result AtualizarFilme(int id, UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return Result.Fail("Filme não encontrado");
            
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletarFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return Result.Fail("Filme não encontrado");

            _context.Remove(filme);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}