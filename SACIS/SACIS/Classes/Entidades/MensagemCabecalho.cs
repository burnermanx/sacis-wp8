///<summary>
/// Classe para objetos do tipo mensagemCabecalho
///
/// @author Fabio Augusto
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SACIS.Classes.Entidades
{
    public class mensagemCabecalho : IEquatable<mensagemCabecalho>
    {
        private int _codigo;
        private DateTime _data;
        private string _assunto;
        private string _tipo;
        private bool _lida;
        private int _tamanho;
        private string _logremdest;

        ///<summary>
        ///
        /// Método contrutor para inicializar o objeto
        ///
        ///</summary>
        public mensagemCabecalho() { }

        ///<summary>
        ///
        /// Método contrutor para inicializar o objeto atraves de parâmetros
        ///
        ///</summary>
        public mensagemCabecalho(int codigo, DateTime data, string assunto, string tipo, bool lida, int tamanho, string logremdest)
        {
            _codigo = codigo;
            _data = data;
            _assunto = assunto;
            _tipo = tipo;
            _lida = lida;
            _tamanho = tamanho;
            _logremdest = logremdest;
        }

        ///<summary>
        ///
        /// Métodos gets e sets usados para
        /// a serialização e deserialização do objeto em XML
        ///
        ///</summary>
        public int codigo
        {

            get { return _codigo; }

            set { _codigo = value; }

        }

        public DateTime data
        {

            get { return _data; }

            set { _data = value; }

        }

        public string assunto
        {

            get { return _assunto; }

            set { _assunto = value; }

        }

        public string tipo
        {

            get { return _tipo; }

            set { _tipo = value; }

        }

        public bool lida
        {

            get { return _lida; }

            set { _lida = value; }

        }

        public int tamanho
        {

            get { return _tamanho; }

            set { _tamanho = value; }

        }

        public string logremdest
        {

            get { return _logremdest; }

            set { _logremdest = value; }

        }

        ///<summary>
        ///
        /// Método get para o codigo
        ///
        ///</summary>
        public int getCodigo()
        {
            return _codigo;
        }

        ///<summary>
        ///
        /// Método set para o codigo
        ///
        ///</summary>
        public void setCodigo(int codigo)
        {
            _codigo = codigo;
        }

        ///<summary>
        ///
        /// Método get para a data
        ///
        ///</summary>
        public DateTime getData()
        {
            return _data;
        }

        ///<summary>
        ///
        /// Método set para a data
        ///
        ///</summary>
        public void setData(DateTime data)
        {
            _data = data;
        }

        ///<summary>
        ///
        /// Método get para o assunto
        ///
        ///</summary>
        public string getAssunto()
        {
            return _assunto;
        }

        ///<summary>
        ///
        /// Método set para o assunto
        ///
        ///</summary>
        public void setAssunto(string assunto)
        {
            _assunto = assunto;
        }

        ///<summary>
        ///
        /// Método get para o tipo
        ///
        ///</summary>
        public string getTipo()
        {
            return _tipo;
        }

        ///<summary>
        ///
        /// Método set para o tipo
        ///
        ///</summary>
        public void setTipo(string tipo)
        {
            _tipo = tipo;
        }

        ///<summary>
        ///
        /// Método get para saber se a mensagem foi lida
        ///
        ///</summary>
        public bool getLida()
        {
            return _lida;
        }

        ///<summary>
        ///
        /// Método set para saber se a mensagem foi lida
        ///
        ///</summary>
        public void setLida(bool lida)
        {
            _lida = lida;
        }

        ///<summary>
        ///
        /// Método get para o tamanho da mensagem
        ///
        ///</summary>
        public int getTamanho()
        {
            return _tamanho;
        }

        ///<summary>
        ///
        /// Método set para o tamanho da mensagem
        ///
        ///</summary>
        public void setTamanho(int tamanho)
        {
            _tamanho = tamanho;
        }

        ///<summary>
        ///
        /// Método get para destinatario/remetente da mensagem
        ///
        ///</summary>
        public string getLogremdest()
        {
            return _logremdest;
        }

        ///<summary>
        ///
        /// Método set para destinatario/remetente da mensagem
        ///
        ///</summary>
        public void setLogremdest(string logremdest)
        {
            _logremdest = logremdest;
        }

        ///<summary>
        ///
        /// Método de sobrecarga para comparação de objetos
        ///
        ///</summary>
        public bool Equals(mensagemCabecalho other)
        {
            if (Object.ReferenceEquals(other, null)) return false;

            if (Object.ReferenceEquals(this, other)) return true;

            return _codigo.Equals(other._codigo);
        }

        ///<summary>
        ///
        /// Método de sobrecarga para obter o hash do objeto
        ///
        ///</summary>
        public override int GetHashCode()
        {
            int hashData = _data.GetHashCode();

            int hashCodigo = _codigo.GetHashCode();

            return hashData ^ hashCodigo;
        }

    }
}
