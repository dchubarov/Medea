// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Filter.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.DataAccess
{
    /// <summary>
    /// Enumeration for filter parts combination values.
    /// </summary>
    public enum FilterCombination
    {
        And,
        Or
    }

    /// <summary>
    /// Enumeration for filter condition values.
    /// </summary>
    public enum FilterCondition
    {
        Equal,
        EqualRef,
        NotEqual,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual,
        Like,
        NotLike,
        OrderAscending,
        OrderDescending
    }

    /// <summary>
    /// Represents search condition or graph of search conditions.
    /// </summary>
    public class Filter
    {
        #region Private Fields

        // combination with preceding filter
        private FilterCombination _combination = FilterCombination.And;
        private bool _combinationNegation = false;

        // complex search condition
        private List<Filter> _filters = null;

        // simple search condition
        private string _field = null;
        private object _criteria = null;
        private FilterCondition _condition = FilterCondition.Equal;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Filter object.
        /// </summary>
        public Filter()
        {
            // create container for child filters
            this.ConvertToComplex();
        }

        public Filter(string field, FilterCondition condition, object criteria)
        {
            _field = field;
            _criteria = criteria;
            _condition = condition;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating that current filter is 
        /// complex e.g. contains sub-filter(s).
        /// </summary>
        public bool IsComplex
        {
            get
            {
                return (_filters != null);
            }
        }

        /// <summary>
        /// Gets a value specifies how the current filter combines with its predecessor.
        /// </summary>
        public FilterCombination Combination
        {
            get
            {
                return _combination;
            }

            set
            {
                _combination = value;
            }
        }

        /// <summary>
        /// Gets a value indicating negation of filter result.
        /// </summary>
        public bool CombinationNegation
        {
            get
            {
                return _combinationNegation;
            }

            set
            {
                _combinationNegation = value;
            }
        }

        /// <summary>
        /// Gets a list of child filters.
        /// </summary>
        public List<Filter> Filters
        {
            get
            {
                if (!this.IsComplex) {
                    throw new InvalidOperationException();
                }

                return _filters;
            }
        }

        /// <summary>
        /// Gets or sets field name of a simple filter.
        /// </summary>
        public string Field
        {
            get
            {
                if (this.IsComplex) {
                    throw new InvalidOperationException();
                }

                return _field;
            }

            set
            {
                if (this.IsComplex) {
                    this.ConvertToSimple();
                }

                _field = value;
            }
        }

        /// <summary>
        /// Gets or sets condition for a simple filter.
        /// </summary>
        public FilterCondition Condition
        {
            get
            {
                if (this.IsComplex) {
                    throw new InvalidOperationException();
                }

                return _condition;
            }

            set
            {
                if (this.IsComplex) {
                    this.ConvertToSimple();
                }

                _condition = value;
            }
        }

        /// <summary>
        /// Gets or sets criteria of a simple filter.
        /// </summary>
        public object Criteria
        {
            get
            {
                if (this.IsComplex) {
                    throw new InvalidOperationException();
                }

                return _criteria;
            }

            set
            {
                if (this.IsComplex) {
                    this.ConvertToSimple();
                }

                _criteria = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts current filter to simple form.
        /// </summary>
        private void ConvertToSimple()
        {
            if (_filters != null) {
                _filters.Clear();
                _filters = null;
            }
        }

        /// <summary>
        /// Converts current filter to complex form.
        /// </summary>
        private void ConvertToComplex()
        {
            if (_filters == null) {
                _filters = new List<Filter>();
            }

            _field = null;
            _criteria = null;
            _condition = FilterCondition.Equal;
        }

        #endregion
    }
}