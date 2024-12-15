﻿using cotto_system.Modelos;

namespace cotto_system.interfaces
{
    public interface IRepositorioCatalogos
    {
        Task addClient(Clientes clientes);
        Task<int> AddValorUnidad(AddVentaUnidad addVentaUnidad);
        Task<IEnumerable<GetClases>> getClases();
        Task<IEnumerable<getGradosCalificacion>> getGradosClasificacion();
        Task<IEnumerable<PerfilVentaDet>> getPerfilVentaDet(int idperfilenc);
        Task<IEnumerable<PerfilVentaEnc>> getPerfilVentaEnc();
        Task<IEnumerable<UnidadVenta>> getUnidadVenta();
    }
}