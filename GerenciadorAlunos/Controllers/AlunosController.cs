using GerenciadorAlunos.Models;
using GerenciadorAlunos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorAlunos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
         _alunoService = alunoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
    {
        try
        {
            var alunos = await _alunoService.GetAlunos();
            return Ok(alunos);
        }
        catch
        {
            return BadRequest("Erro ao executar operação");
        }
    }

    [HttpGet("/AlunoNome")]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoByName(string nome)
    {
        try
        {
            var aluno = await _alunoService.GetAlunosByNome(nome);
            if (aluno.Count() == 0)
                return NotFound($"Aluno {nome} não encontrado !");
            return Ok(aluno);
        }
        catch
        {
            return BadRequest("Erro ao executar operação");
        }
    }

    [HttpGet("{id:int}", Name = "GetAluno")]
    public async Task<ActionResult<Aluno>> GetAluno(int id)
    {
        try
        {
            var aluno = await _alunoService.GetAluno(id);
            if (aluno is null)
                return NotFound($"Aluno id {id} não encontrado !");
            return Ok(aluno);
        }
        catch
        {
            return BadRequest("Erro ao executar operação");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(Aluno aluno)
    {
        try
        {
            await _alunoService.CreateAluno(aluno);
            return CreatedAtRoute(nameof(GetAluno), new {id = aluno.Id}, aluno);
        }
        catch
        {
            return BadRequest("Erro ao executar operação");
        }
        
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Aluno aluno)
    {
        try
        {
            if(aluno.Id == id)
            {
                await _alunoService.UpdateAluno(aluno);
                return Ok($"Aluno id {id} foi atualizado");
            }
            else
            {
                return BadRequest("Dados inconsistentes");
            }

        }
        catch
        {
            return BadRequest("Erro ao executar operação");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var aluno = await _alunoService.GetAluno(id);
            if (aluno == null)
                return NotFound($"Aluno id {id} não encontrado");

            await _alunoService.DeleteAluno(aluno);
            return Ok($"Aluno id {aluno.Id} deletado com sucesso");
        }
        catch
        {

            return BadRequest("Erro ao executar operação");
        }
    }
}
