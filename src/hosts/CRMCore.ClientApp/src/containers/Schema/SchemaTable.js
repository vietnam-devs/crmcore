import React, { Component } from 'react';
import ReactTable from 'react-table';

const columnsRender = (selectAll, selected, toggleSelectAll, toggleRow) => {
  return [
    {
      id: 'checkbox',
      accessor: '',
      Header: x => {
        return (
          <input
            key={x.id}
            type="checkbox"
            className="checkbox"
            checked={selectAll === 1}
            ref={input => {
              if (input) {
                input.indeterminate = selectAll === 2;
              }
            }}
            onChange={() => toggleSelectAll()}
          />
        );
      },
      Cell: ({ original }) => {
        return (
          <input
            key={original.id}
            type="checkbox"
            className="checkbox"
            checked={selected[original.id] === true}
            onChange={() => toggleRow(original.id)}
          />
        );
      },
      filterable: false,
      sortable: false,
      width: 45
    },
    {
      Header: 'Name',
      accessor: 'name'
    },
    {
      Header: 'Published',
      accessor: 'isPublished'
    }
  ];
};

export default class BlogTable extends Component {
  constructor(props) {
    super(props);

    this.state = {
      selected: {},
      selectAll: 0
    };

    // fetch data on every changes on table
    this.fetchData = this.fetchData.bind(this);

    // event for checkbox in the table
    this.toggleRow = this.toggleRow.bind(this);
    this.toggleSelectAll = this.toggleSelectAll.bind(this);
  }

  fetchData(state, instance) {
    this.props.loadSchemas(state.page);
  }

  toggleRow(id) {
    const newSelected = { ...this.state.selected };
    newSelected[id] = !this.state.selected[id];
    this.setState({
      selected: newSelected,
      selectAll: 2
    });
  }

  toggleSelectAll() {
    let newSelected = {};
    if (this.state.selectAll === 0) {
      this.props.schemas.map(schema => {
        newSelected[schema.name] = true;
      });
    }

    this.setState({
      selected: newSelected,
      selectAll: this.state.selectAll === 0 ? 1 : 0
    });
  }

  render() {
    console.log(this.props.schemas);
    const columns = columnsRender(
      this.state.selectAll,
      this.state.selected,
      this.toggleSelectAll,
      this.toggleRow
    );

    const table = (
      <ReactTable
        columns={columns}
        manual
        data={this.props.schemas}
        className="-striped -highlight"
        defaultPageSize={10}
        showPageSizeOptions={false}
        filterable
        defaultSorted={[
          {
            id: 'name',
            desc: true
          }
        ]}
        pages={this.props.totalPages}
        loading={this.props.loading}
        onFetchData={this.fetchData}
        getTdProps={(state, rowInfo, column, instance) => {
          return {
            onClick: (event, handleOriginal) => {
              console.log(rowInfo.original.name);
              console.log(this.props.history);
              this.props.history.push(
                `/metadata/schema-form/${rowInfo.original.name}`
              );
              if (handleOriginal) {
                handleOriginal();
              }
            }
          };
        }}
        previousText={<i className="icon-arrow-left" />}
        nextText={<i className="icon-arrow-right" />}
      />
    );

    return table;
  }
}
